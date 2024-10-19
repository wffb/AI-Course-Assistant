import React, { useState, useEffect } from 'react';
import { Container, Table, Button, Modal, Form, Alert, ProgressBar } from 'react-bootstrap';
import { PlusCircle, Edit, Trash, Upload, CheckCircle, XCircle } from 'lucide-react';
import BackToDashboardButton from '../components/BackToDash';
import LogoutButton from '../components/LogOut';

const backendUrl = "http://localhost:8080";

const gptModels = [
  'gpt-4o-mini', 'gpt-4o', 'gpt-4-turbo', 'gpt-4', 'gpt-3.5-turbo',
  'gpt-4o-mini-2024-07-18', 'gpt-4o-2024-08-06', 'gpt-4o-2024-05-13',
  'gpt-4-turbo-preview', 'gpt-4-turbo-2024-04-09', 'gpt-4-1106-preview'
];

export default function AIAssistantManagementPage() {
  const [assistants, setAssistants] = useState([]);
  const [showAddModal, setShowAddModal] = useState(false);
  const [showUpdateModal, setShowUpdateModal] = useState(false);
  const [showUploadModal, setShowUploadModal] = useState(false);
  const [currentAssistant, setCurrentAssistant] = useState({
    id: '',
    name: '',
    description: '',
    instructions: '',
    model: '',
    temperature: 1,
    topP: 0.45,
    subjectId: '',
    identifyId: ''
  });
  const [subjects, setSubjects] = useState([]);
  const [error, setError] = useState(null);
  const [successMessage, setSuccessMessage] = useState(null);
  const [selectedAssistant, setSelectedAssistant] = useState(null);
  const [file, setFile] = useState(null);
  const [fileStatus, setFileStatus] = useState(null);

  useEffect(() => {
    fetchAssistants();
    fetchSubjects();
  }, []);

  const fetchAssistants = async () => {
    try {
      const response = await fetch(`${backendUrl}/admin/assistant/getAllAssistants`, {
        headers: {
          'Authorization': localStorage.getItem('token')
        }
      });
      if (!response.ok) throw new Error('Failed to fetch assistants');
      const result = await response.json();
      if (result.code === 200 && Array.isArray(result.data)) {
        setAssistants(result.data);
      } else {
        throw new Error('Invalid data format');
      }
    } catch (err) {
      setError('Failed to load assistants. Please try again later.');
    }
  };

  const fetchSubjects = async () => {
    try {
      const response = await fetch(`${backendUrl}/admin/assistant/getSubjects`, {
        headers: {
          'Authorization': localStorage.getItem('token')
        }
      });
      if (!response.ok) throw new Error('Failed to fetch subjects');
      const result = await response.json();
      if (result.code === 200 && Array.isArray(result.data)) {
        setSubjects(result.data);
      } else {
        throw new Error('Invalid subject data format');
      }
    } catch (err) {
      setError('Failed to load subjects. Please try again later.');
    }
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setCurrentAssistant(prev => ({ ...prev, [name]: value }));
  };

  const handleFileUpload = (e) => {
    setFile(e.target.files[0]);
  };

  const handleAddAssistant = async () => {
    try {
      const assistantData = {
        name: currentAssistant.name,
        model: currentAssistant.model,
        temperature: parseFloat(currentAssistant.temperature),
        topP: parseFloat(currentAssistant.topP),
        instructions: currentAssistant.instructions,
        description: currentAssistant.description,
        subjectId: currentAssistant.subjectId
      };

      const response = await fetch(`${backendUrl}/admin/assistant/createAssistant`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': localStorage.getItem('token')
        },
        body: JSON.stringify(assistantData)
      });

      const result = await response.json();

      if (result.code === 200) {
        setAssistants(prev => [...prev, result.data]);
        setShowAddModal(false);
        setCurrentAssistant({
          id: '',
          name: '',
          description: '',
          instructions: '',
          model: '',
          temperature: 1,
          topP: 0.45,
          subjectId: '',
          identifyId: ''
        });
        setSuccessMessage('Assistant created successfully!');
        setTimeout(() => setSuccessMessage(null), 3000);
      } else {
        throw new Error(result.message || 'Failed to add assistant');
      }
    } catch (err) {
      setError(err.message || 'Failed to add assistant. Please try again.');
    }
  };

  const handleUpdateAssistant = async () => {
    try {
      const assistantData = {
        identifyId: currentAssistant.identifyId,
        name: currentAssistant.name,
        model: currentAssistant.model,
        temperature: parseFloat(currentAssistant.temperature),
        topP: parseFloat(currentAssistant.topP),
        instructions: currentAssistant.instructions,
        description: currentAssistant.description,
        subjectId: currentAssistant.subjectId
      };

      const response = await fetch(`${backendUrl}/admin/assistant/updateAssistant`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': localStorage.getItem('token')
        },
        body: JSON.stringify(assistantData)
      });

      const result = await response.json();

      if (result.code === 200) {
        setAssistants(prev => prev.map(assistant => 
          assistant.identifyId === currentAssistant.identifyId ? currentAssistant : assistant
        ));
        setShowUpdateModal(false);
        setSuccessMessage('Assistant updated successfully!');
        setTimeout(() => setSuccessMessage(null), 3000);
      } else {
        throw new Error(result.message || 'Failed to update assistant');
      }
    } catch (err) {
      setError(err.message || 'Failed to update assistant. Please try again.');
    }
  };

  const handleUpload = async () => {
    if (!file || !selectedAssistant) {
      alert('Please select a file and an AI assistant');
      return;
    }

    setFileStatus({
      name: file.name,
      status: 'uploading',
      progress: 0
    });

    const formData = new FormData();
    formData.append('file', file);
    formData.append('assistantId', selectedAssistant.identifyId);

    try {
      const response = await fetch(`${backendUrl}/admin/uploadFile`, {
        method: 'POST',
        headers: {
          'Authorization': localStorage.getItem('token')
        },
        body: formData
      });

      const result = await response.json();

      if (result.code === 200) {
        setFileStatus({
          name: file.name,
          status: 'success',
          progress: 100,
          message: `File uploaded successfully. ID: ${result.data.id}, Date: ${result.data.date}`
        });
      } else {
        throw new Error(result.message || 'Upload failed');
      }
    } catch (error) {
      setFileStatus({
        name: file.name,
        status: 'error',
        progress: 100,
        message: error.message
      });
    }
  };

  return (
    <Container className="mt-4">
      <BackToDashboardButton/>
      <LogoutButton />
      <h1 className="mb-4">AI Assistant Management</h1>
      {error && <Alert variant="danger">{error}</Alert>}
      {successMessage && <Alert variant="success">{successMessage}</Alert>}
      <Button variant="primary" className="mb-3" onClick={() => setShowAddModal(true)}>
        <PlusCircle size={18} className="me-2" />
        Add New Assistant
      </Button>
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>Name</th>
            <th>Description</th>
            <th>Model</th>
            <th>Temperature</th>
            <th>Top P</th>
            <th>Subject ID</th>
            <th>Identify ID</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {assistants.map(assistant => (
            <tr key={assistant.id}>
              <td>{assistant.name}</td>
              <td>{assistant.description}</td>
              <td>{assistant.model}</td>
              <td>{assistant.temperature}</td>
              <td>{assistant.topP}</td>
              <td>{assistant.subjectId}</td>
              <td>{assistant.identifyId}</td>
              <td>
                <Button variant="outline-primary" size="sm" className="me-2" onClick={() => {
                  setCurrentAssistant(assistant);
                  setShowUpdateModal(true);
                }}>
                  <Edit size={18} />
                </Button>
                <Button variant="outline-danger" size="sm" className="me-2">
                  <Trash size={18} />
                </Button>
                <Button 
                  variant="outline-success" 
                  size="sm"
                  onClick={() => {
                    setSelectedAssistant(assistant);
                    setShowUploadModal(true);
                  }}
                >
                  <Upload size={18} />
                </Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>

      <Modal show={showAddModal} onHide={() => setShowAddModal(false)} size="lg">
        <Modal.Header closeButton>
          <Modal.Title>Add New AI Assistant</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group className="mb-3">
              <Form.Label>Name</Form.Label>
              <Form.Control 
                type="text" 
                name="name"
                value={currentAssistant.name} 
                onChange={handleInputChange}
              />
            </Form.Group>
            <Form.Group className="mb-3">
              <Form.Label>Model</Form.Label>
              <Form.Control 
                as="select"
                name="model"
                value={currentAssistant.model}
                onChange={handleInputChange}
              >
                <option value="">Select a model</option>
                {gptModels.map(model => (
                  <option key={model} value={model}>{model}</option>
                ))}
              </Form.Control>
            </Form.Group>
            <Form.Group className="mb-3">
              <Form.Label>Temperature: {currentAssistant.temperature}</Form.Label>
              <Form.Control 
                type="range" 
                name="temperature"
                min="0"
                max="2"
                step="0.1"
                value={currentAssistant.temperature}
                onChange={handleInputChange}
              />
            </Form.Group>
            <Form.Group className="mb-3">
              <Form.Label>Top P: {currentAssistant.topP}</Form.Label>
              <Form.Control 
                type="range" 
                name="topP"
                min="0"
                max="1"
                step="0.01"
                value={currentAssistant.topP}
                onChange={handleInputChange}
              />
            </Form.Group>
            <Form.Group className="mb-3">
              <Form.Label>Instruction</Form.Label>
              <Form.Control 
                as="textarea" 
                rows={3}
                name="instructions"
                value={currentAssistant.instructions}
                onChange={handleInputChange}
              />
            </Form.Group>
            <Form.Group className="mb-3">
              <Form.Label>Description</Form.Label>
              <Form.Control 
                type="text" 
                name="description"
                value={currentAssistant.description} 
                onChange={handleInputChange}
              />
            </Form.Group>
            <Form.Group className="mb-3">
              <Form.Label>Subject</Form.Label>
              <Form.Control 
                as="select"
                name="subjectId"
                value={currentAssistant.subjectId}
                onChange={handleInputChange}
              >
                <option value="">Select a subject</option>
                {subjects.map(subject => (
                  <option key={subject.id} value={subject.identifyId}>{subject.name} ({subject.identifyId})</option>
                ))}
              </Form.Control>
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={() => setShowAddModal(false)}>
            Close
          </Button>
          <Button variant="primary" onClick={handleAddAssistant}>
            Add Assistant
          </Button>
        </Modal.Footer>
      </Modal>

      <Modal show={showUpdateModal} onHide={() => setShowUpdateModal(false)} size="lg">
        <Modal.Header closeButton>
          <Modal.Title>Update AI Assistant</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group className="mb-3">
              <Form.Label>Name</Form.Label>
              <Form.Control 
                type="text" 
                name="name"
                value={currentAssistant.name} 
                onChange={handleInputChange}
              />
            </Form.Group>
            <Form.Group className="mb-3">
              <Form.Label>Model</Form.Label>
              <Form.Control 
                as="select"
                name="model"
                value={currentAssistant.model}
                onChange={handleInputChange}
              >
                <option value="">Select a model</option>
                {gptModels.map(model => (
                  <option key={model} value={model}>{model}</option>
                ))}
              </Form.Control>
            </Form.Group>
            <Form.Group className="mb-3">
              <Form.Label>Temperature: {currentAssistant.temperature}</Form.Label>
              <Form.Control 
                type="range" 
                name="temperature"
                min="0"
                max="2"
                
                step="0.1"
                value={currentAssistant.temperature}
                onChange={handleInputChange}
              />
            </Form.Group>
            <Form.Group className="mb-3">
              <Form.Label>Top P: {currentAssistant.topP}</Form.Label>
              <Form.Control 
                type="range" 
                name="topP"
                min="0"
                max="1"
                step="0.01"
                value={currentAssistant.topP}
                onChange={handleInputChange}
              />
            </Form.Group>
            <Form.Group className="mb-3">
              <Form.Label>Instruction</Form.Label>
              <Form.Control 
                as="textarea" 
                rows={3}
                name="instructions"
                value={currentAssistant.instructions}
                onChange={handleInputChange}
              />
            </Form.Group>
            <Form.Group className="mb-3">
              <Form.Label>Description</Form.Label>
              <Form.Control 
                type="text" 
                name="description"
                value={currentAssistant.description} 
                onChange={handleInputChange}
              />
            </Form.Group>
            <Form.Group className="mb-3">
              <Form.Label>Subject</Form.Label>
              <Form.Control 
                as="select"
                name="subjectId"
                value={currentAssistant.subjectId}
                onChange={handleInputChange}
              >
                <option value="">Select a subject</option>
                {subjects.map(subject => (
                  <option key={subject.id} value={subject.identifyId}>{subject.name} ({subject.identifyId})</option>
                ))}
              </Form.Control>
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={() => setShowUpdateModal(false)}>
            Close
          </Button>
          <Button variant="primary" onClick={handleUpdateAssistant}>
            Update Assistant
          </Button>
        </Modal.Footer>
      </Modal>

      <Modal show={showUploadModal} onHide={() => setShowUploadModal(false)}>
        <Modal.Header closeButton>
          <Modal.Title>Upload File for {selectedAssistant?.name}</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group controlId="formFile" className="mb-3">
              <Form.Label>Upload course-related file</Form.Label>
              <Form.Control type="file" onChange={handleFileUpload} />
            </Form.Group>
            <Button 
              variant="primary" 
              onClick={handleUpload} 
              disabled={!file}
            >
              <Upload size={18} className="me-2" />
              Upload File
            </Button>
          </Form>
          {fileStatus && (
            <div className="mt-3">
              <div className="d-flex justify-content-between align-items-center mb-2">
                <span>{fileStatus.name}</span>
                {fileStatus.status === 'uploading' && <span>Uploading...</span>}
                {fileStatus.status === 'success' && <CheckCircle size={18} className="text-success" />}
                {fileStatus.status === 'error' && <XCircle size={18} className="text-danger" />}
              </div>
              <ProgressBar 
                now={fileStatus.progress} 
                variant={fileStatus.status === 'error' ? 'danger' : 'primary'} 
                className="mb-2"
              />
              {fileStatus.message && (
                <Alert variant={fileStatus.status === 'success' ? 'success' : 'danger'}>
                  {fileStatus.message}
                </Alert>
              )}
            </div>
          )}
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={() => setShowUploadModal(false)}>
            Close
          </Button>
        </Modal.Footer>
      </Modal>
    </Container>
  );
}