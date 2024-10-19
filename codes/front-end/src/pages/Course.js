import React, { useState, useEffect } from 'react';
import { Container, Table, Button, Modal, Form, Alert, ListGroup } from 'react-bootstrap';
import { PlusCircle, Trash, Info, Edit } from 'lucide-react';
import BackToDashboardButton from '../components/BackToDash';
import LogoutButton from '../components/LogOut';

const backendUrl = "http://localhost:8080";

export default function CourseManagementPage() {
  const [courses, setCourses] = useState([]);
  const [showAddModal, setShowAddModal] = useState(false);
  const [showUpdateModal, setShowUpdateModal] = useState(false);
  const [showInfoModal, setShowInfoModal] = useState(false);
  const [selectedCourse, setSelectedCourse] = useState(null);
  const [newCourse, setNewCourse] = useState({ name: '', identifyId: '', description: 'Add description here' });
  const [error, setError] = useState(null);
  const [successMessage, setSuccessMessage] = useState(null);
  const [aiAssistants, setAiAssistants] = useState([]);

  useEffect(() => {
    fetchCourses();
  }, []);

  const fetchCourses = async () => {
    try {
      const response = await fetch(`${backendUrl}/admin/assistant/getSubjects`, {
        headers: {
          'Authorization': localStorage.getItem('token')
        }
      });
      if (!response.ok) throw new Error('Failed to fetch courses');
      const result = await response.json();
      if (result.code === 200 && Array.isArray(result.data)) {
        setCourses(result.data);
      } else {
        throw new Error('Invalid data format');
      }
    } catch (err) {
      setError('Failed to load courses. Please try again later.');
    }
  };

  const fetchAiAssistants = async (subjectId) => {
    try {
      const response = await fetch(`${backendUrl}/admin/assistant/getAssistantBySubjectId?subjectIdentifyId=${subjectId}`, {
        headers: {
          'Authorization': localStorage.getItem('token')
        }
      });
      if (!response.ok) throw new Error('Failed to fetch AI assistants');
      const result = await response.json();
      if (result.code === 200 && Array.isArray(result.data)) {
        setAiAssistants(result.data);
      } else {
        throw new Error('Invalid data format');
      }
    } catch (err) {
      setError('Failed to load AI assistants. Please try again later.');
    }
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setNewCourse(prev => ({ ...prev, [name]: value }));
  };

  const handleAddCourse = async () => {
    if (courses.some(course => course.name === newCourse.name || course.identifyId === newCourse.identifyId)) {
      setError('A course with this name or ID already exists.');
      return;
    }

    try {
      const response = await fetch(`${backendUrl}/admin/subject/add`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': localStorage.getItem('token')
        },
        body: JSON.stringify(newCourse)
      });

      const result = await response.json();

      if (result.code === 200) {
        fetchCourses(); 
        setShowAddModal(false);
        setNewCourse({ name: '', identifyId: '', description: 'Add description here' });
        setSuccessMessage('Course added successfully!');
        setTimeout(() => setSuccessMessage(null), 3000);
      } else {
        throw new Error(result.message || 'Failed to add course');
      }
    } catch (err) {
      setError(err.message || 'Failed to add course. Please try again.');
    }
  };

  const handleUpdateCourse = async () => {
    try {
      const response = await fetch(`${backendUrl}/admin/subject/update`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': localStorage.getItem('token')
        },
        body: JSON.stringify({
          id: selectedCourse.id,
          name: newCourse.name,
          identifyId: newCourse.identifyId,
          description: newCourse.description
        })
      });

      const result = await response.json();

      if (result.code === 200) {
        fetchCourses(); 
        setShowUpdateModal(false);
        setSuccessMessage('Course updated successfully!');
        setTimeout(() => setSuccessMessage(null), 3000);
      } else {
        throw new Error(result.message || 'Failed to update course');
      }
    } catch (err) {
      setError(err.message || 'Failed to update course. Please try again.');
    }
  };

  const handleDeleteCourse = async (identifyId) => {
    try {
      const response = await fetch(`${backendUrl}/admin/subject/delete/${identifyId}`, {
        method: 'DELETE',
        headers: {
          'Authorization': localStorage.getItem('token')
        }
      });

      const result = await response.json();

      if (result.code === 200) {
        setCourses(prev => prev.filter(course => course.identifyId !== identifyId));
        setSuccessMessage('Course deleted successfully!');
        setTimeout(() => setSuccessMessage(null), 3000);
      } else {
        throw new Error(result.message || 'Failed to delete course');
      }
    } catch (err) {
      setError(err.message || 'Failed to delete course. Please try again.');
    }
  };

  const handleCourseClick = async (course) => {
    setSelectedCourse(course);
    await fetchAiAssistants(course.identifyId);
    setShowInfoModal(true);
  };

  const handleEditClick = (course) => {
    setSelectedCourse(course);
    setNewCourse({ 
      name: course.name, 
      identifyId: course.identifyId, 
      description: course.description || 'Add description here' 
    });
    setShowUpdateModal(true);
  };

  return (
    <Container className="mt-4">
      <BackToDashboardButton />
      <LogoutButton />
      <h1 className="mb-4">Course Management</h1>
      {error && <Alert variant="danger">{error}</Alert>}
      {successMessage && <Alert variant="success">{successMessage}</Alert>}
      <Button variant="primary" className="mb-3" onClick={() => setShowAddModal(true)}>
        <PlusCircle size={18} className="me-2" />
        Add New Course
      </Button>
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>Course Name</th>
            <th>Course ID</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {courses.map(course => (
            <tr key={course.identifyId}>
              <td>
                <Button variant="link" onClick={() => handleCourseClick(course)}>
                  {course.name}
                </Button>
              </td>
              <td>{course.identifyId}</td>
              <td>
                <Button variant="outline-info" size="sm" className="me-2" onClick={() => handleCourseClick(course)}>
                  <Info size={18} />
                </Button>
                <Button variant="outline-primary" size="sm" className="me-2" onClick={() => handleEditClick(course)}>
                  <Edit size={18} />
                </Button>
                <Button variant="outline-danger" size="sm" onClick={() => handleDeleteCourse(course.identifyId)}>
                  <Trash size={18} />
                </Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>

      <Modal show={showAddModal} onHide={() => setShowAddModal(false)}>
        <Modal.Header closeButton>
          <Modal.Title>Add New Course</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group className="mb-3">
              <Form.Label>Course Name</Form.Label>
              <Form.Control 
                type="text" 
                name="name"
                value={newCourse.name} 
                onChange={handleInputChange}
              />
            </Form.Group>
            <Form.Group className="mb-3">
              <Form.Label>Course ID</Form.Label>
              <Form.Control 
                type="text" 
                name="identifyId"
                value={newCourse.identifyId} 
                onChange={handleInputChange}
              />
            </Form.Group>
            <Form.Group className="mb-3">
              <Form.Label>Description</Form.Label>
              <Form.Control 
                as="textarea" 
                rows={3}
                name="description"
                value={newCourse.description} 
                onChange={handleInputChange}
              />
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={() => setShowAddModal(false)}>
            Close
          </Button>
          <Button variant="primary" onClick={handleAddCourse}>
            Add Course
          </Button>
        </Modal.Footer>
      </Modal>

      <Modal show={showUpdateModal} onHide={() => setShowUpdateModal(false)}>
        <Modal.Header closeButton>
          <Modal.Title>Update Course</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group className="mb-3">
              <Form.Label>Course Name</Form.Label>
              <Form.Control 
                type="text" 
                name="name"
                value={newCourse.name} 
                onChange={handleInputChange}
              />
            </Form.Group>
            <Form.Group className="mb-3">
              <Form.Label>Course ID</Form.Label>
              <Form.Control 
                type="text" 
                name="identifyId"
                value={newCourse.identifyId} 
                onChange={handleInputChange}
              />
            </Form.Group>
            <Form.Group className="mb-3">
              <Form.Label>Description</Form.Label>
              <Form.Control 
                as="textarea" 
                rows={3}
                name="description"
                value={newCourse.description} 
                onChange={handleInputChange}
              />
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={() => setShowUpdateModal(false)}>
            Close
          </Button>
          <Button variant="primary" onClick={handleUpdateCourse}>
            Update Course
          </Button>
        </Modal.Footer>
      </Modal>

      <Modal show={showInfoModal} onHide={() => setShowInfoModal(false)}>
        <Modal.Header closeButton>
          <Modal.Title>Course Information</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          {selectedCourse && (
            <>
              <p><strong>Course Name:</strong> {selectedCourse.name}</p>
              <p><strong>Course ID:</strong> {selectedCourse.identifyId}</p>
              <p><strong>Description:</strong> {selectedCourse.description || 'No description available'}</p>
              <h5>AI Assistants:</h5>
              <ListGroup>
                {aiAssistants.map(assistant => (
                  <ListGroup.Item key={assistant.id}>
                    {assistant.name} - {assistant.model}
                  </ListGroup.Item>
                ))}
              </ListGroup>
            </>
          )}
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={() => setShowInfoModal(false)}>
            Close
          </Button>
        </Modal.Footer>
      </Modal>
    </Container>
  );
}