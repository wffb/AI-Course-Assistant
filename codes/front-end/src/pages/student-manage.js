import React, { useState, useEffect } from 'react';
import { Container, Table, Button, Modal, Form, Alert } from 'react-bootstrap';
import { Trash, PlusCircle, Edit } from 'lucide-react';
import BackToDashboardButton from '../components/BackToDash';
import LogoutButton from '../components/LogOut';

const backendUrl = "http://localhost:8080";

export default function StudentManagementPage() {
  const [students, setStudents] = useState([]);
  const [showModal, setShowModal] = useState(false);
  const [currentStudent, setCurrentStudent] = useState(null);
  const [newSubjectId, setNewSubjectId] = useState('');
  const [error, setError] = useState(null);
  const [apiError, setApiError] = useState(null);

  useEffect(() => {
    fetchStudents();
  }, []);

  const fetchStudents = async () => {
    try {
      const response = await fetch(`${backendUrl}/admin/getUserList`, {
        headers: {
          'Authorization': localStorage.getItem('token')
        }
      });
      if (!response.ok) throw new Error('Failed to fetch students');
      const result = await response.json();
      if (result.code === 200 && Array.isArray(result.data)) {
        const studentList = result.data
          .filter(user => !user.isTeacher)
          .map(user => ({
            id: user.id,
            name: user.username,
            email: '',
            courses: []
          }));
        const updatedStudents = await fetchStudentCourses(studentList);
        setStudents(updatedStudents);
      } else {
        throw new Error('Invalid data format');
      }
    } catch (err) {
      setError('Failed to load students. Please try again later.');
    }
  };

  const fetchStudentCourses = async (studentList) => {
    const updatedStudents = await Promise.all(studentList.map(async (student) => {
      try {
        const response = await fetch(`${backendUrl}/admin/assistant/getSubjectsByUserId?username=${student.name}`, {
          headers: {
            'Authorization': localStorage.getItem('token')
          }
        });
        if (!response.ok) throw new Error(`Failed to fetch courses for ${student.name}`);
        const result = await response.json();
        if (result.code === 200 && Array.isArray(result.data)) {
          return { ...student, courses: result.data.join('/') };
        }
        return student;
      } catch (err) {
        console.error(`Error fetching courses for ${student.name}:`, err);
        return student;
      }
    }));
    return updatedStudents;
  };

  const handleEditStudent = (student) => {
    setCurrentStudent(student);
    setNewSubjectId('');
    setShowModal(true);
  };

  const handleDeleteStudent = (id) => {
    setStudents(students.filter(student => student.id !== id));
  };

  const handleSaveStudent = async () => {
    setApiError(null);
    if (currentStudent && newSubjectId) {
      try {
        const response = await fetch(`${backendUrl}/admin/addStuToSub`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
            'Authorization': localStorage.getItem('token')
          },
          body: JSON.stringify({
            studentId: currentStudent.id,
            subjectIdentifyId: newSubjectId
          })
        });

        const result = await response.json();

        if (result.code !== 200) {
          throw new Error(result.message);
        }

        // Update local state
        const updatedStudents = await fetchStudentCourses([currentStudent]);
        if (updatedStudents && updatedStudents.length > 0) {
          setStudents(prevStudents => prevStudents.map(student => 
            student.id === currentStudent.id ? updatedStudents[0] : student
          ));
        } else {
          throw new Error('Failed to update student courses');
        }

        setShowModal(false);
      } catch (error) {
        setApiError(error.message);
      }
    } else {
      setApiError('Please enter a Subject ID');
    }
  };

  const handleSubjectIdChange = (e) => {
    setNewSubjectId(e.target.value);
  };

  return (
    <Container className="mt-4">
      <BackToDashboardButton/>
      <LogoutButton />
      <h1 className="mb-4">Student Management</h1>
      {error && <Alert variant="danger">{error}</Alert>}
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>Name</th>
            <th>Email</th>
            <th>Courses</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {students.map(student => (
            <tr key={student.id}>
              <td>{student.name}</td>
              <td>{student.email}</td>
              <td>{student.courses}</td>
              <td>
                <Button variant="outline-primary" size="sm" className="me-2" onClick={() => handleEditStudent(student)}>
                  <Edit size={18} />
                </Button>
                <Button variant="outline-danger" size="sm" onClick={() => handleDeleteStudent(student.id)}>
                  <Trash size={18} />
                </Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>

      <Modal show={showModal} onHide={() => setShowModal(false)}>
        <Modal.Header closeButton>
          <Modal.Title>Add Subject to Student</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group className="mb-3">
              <Form.Label>Student Name</Form.Label>
              <Form.Control 
                type="text" 
                value={currentStudent?.name || ''}
                readOnly
              />
            </Form.Group>
            <Form.Group className="mb-3">
              <Form.Label>Subject ID</Form.Label>
              <Form.Control 
                type="text" 
                value={newSubjectId}
                onChange={handleSubjectIdChange}
                placeholder="Enter Subject ID"
              />
            </Form.Group>
          </Form>
          {apiError && <Alert variant="danger" className="mt-3">{apiError}</Alert>}
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={() => setShowModal(false)}>
            Close
          </Button>
          <Button variant="primary" onClick={handleSaveStudent}>
            Add Subject
          </Button>
        </Modal.Footer>
      </Modal>
    </Container>
  );
}