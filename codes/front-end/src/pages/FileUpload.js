import React, { useState, useEffect } from 'react';
import { Container, Form, Button, Alert, ProgressBar, ListGroup } from 'react-bootstrap';
import { Upload, CheckCircle, XCircle } from 'lucide-react';

const backendUrl = "http://localhost:8080";

export default function FileUploadPage() {
  const [file, setFile] = useState(null);
  const [fileStatus, setFileStatus] = useState(null);
  const [assistants, setAssistants] = useState([]);
  const [selectedAssistantId, setSelectedAssistantId] = useState('');

  useEffect(() => {
    fetchAssistants();
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
      console.error('Failed to load assistants:', err);
    }
  };

  const handleFileChange = (event) => {
    if (event.target.files && event.target.files[0]) {
      setFile(event.target.files[0]);
    }
  };

  const handleAssistantChange = (event) => {
    setSelectedAssistantId(event.target.value);
  };

  const handleUpload = async () => {
    if (!file || !selectedAssistantId) {
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
    formData.append('assistantId', selectedAssistantId);

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
      <h1 className="mb-4">File Upload</h1>
      <Form>
        <Form.Group controlId="formAssistant" className="mb-3">
          <Form.Label>Select AI Assistant</Form.Label>
          <Form.Control as="select" value={selectedAssistantId} onChange={handleAssistantChange}>
            <option value="">Choose an AI assistant...</option>
            {assistants.map(assistant => (
              <option key={assistant.id} value={assistant.identifyId}>
                {assistant.name}
              </option>
            ))}
          </Form.Control>
        </Form.Group>
        <Form.Group controlId="formFile" className="mb-3">
          <Form.Label>Upload course-related file</Form.Label>
          <Form.Control type="file" onChange={handleFileChange} />
        </Form.Group>
        <Button 
          variant="primary" 
          onClick={handleUpload} 
          disabled={!file || !selectedAssistantId}
        >
          <Upload size={18} className="me-2" />
          Upload File
        </Button>
      </Form>

      {fileStatus && (
        <ListGroup className="mt-4">
          <ListGroup.Item className="mb-2">
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
          </ListGroup.Item>
        </ListGroup>
      )}
    </Container>
  );
}
