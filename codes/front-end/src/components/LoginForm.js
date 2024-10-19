import React, { useState } from 'react';
import { Card, Form, Button, Modal, Toast } from 'react-bootstrap';
import { useNavigate, Link } from 'react-router-dom';
import './customCss.css';
const backendUrl = "http://localhost:8080";

export default function LoginForm() {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [showErrorModal, setShowErrorModal] = useState(false);
  const [error, setError] = useState('');
  const [showToast, setShowToast] = useState(false);
  const [toastMessage, setToastMessage] = useState('');
  const navigate = useNavigate();

  const handleUsernameChange = (event) => {
    setUsername(event.target.value);
  };

  const handlePasswordChange = (event) => {
    setPassword(event.target.value);
  };

  const handleFormSubmit = async (e) => {
    e.preventDefault();
    if (!username.trim()) {
      setToastMessage('Please enter a username.');
      setShowToast(true);
      return;
    }
  
    if (!password.trim()) {
      setToastMessage('Please enter a password.');
      setShowToast(true);
      return;
    }

    try {
      const response = await fetch((backendUrl +'/login'), {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ username, password }),
      });

      const data = await response.json();

      if (response.ok && data.code === 200) {
        localStorage.setItem('token', data.data.token);
        if (data.data.isTeacher === "true") {
          navigate('/Dashboard');
        } else {
          setError(data.message || 'Login failed. Please try again.');
        }
      } else {
        setError(data.message || 'Login failed. Please try again.');
        setShowErrorModal(true);
      }
    } catch (error) {
      setError('An error occurred. Please try again.');
      setShowErrorModal(true);
    }
  };

  return (
    <div className="d-flex flex-column align-items-center">
      <Card className="custom-card2">
        <Card.Header className="text-center custom-cardheader">Welcome Back</Card.Header>
        <Card.Body>
          <Form onSubmit={handleFormSubmit} data-role="user">
            <Form.Group className="mb-3" controlId="username">
              <Form.Label className="text-center custom-formlabel">Work Email</Form.Label>
              <Form.Control
                type="text"
                placeholder="Work Email"
                maxLength="30"
                className="custom-form-control"
                value={username}
                onChange={handleUsernameChange}
              />
            </Form.Group>
            <Form.Group className="mb-3" controlId="password">
              <Form.Label className="text-center custom-formlabel">Password</Form.Label>
              <Form.Control
                type="password"
                placeholder="Password"
                maxLength="30"
                className="custom-form-control"
                value={password}
                onChange={handlePasswordChange}
              />
            </Form.Group>
            <Button variant="primary" type="submit" className="w-100 custom-button">
              Log in
            </Button>
            <div className="mt-3 d-flex justify-content-between align-items-center">
              <Link to="/login/resetPassword" className="forgot-password-link">Forgot password?</Link>
              <div>
                <span>Haven't had an account?</span> <Link to="/signup">Sign up Now</Link>
              </div>
            </div>
          </Form>
        </Card.Body>
      </Card>
      <Modal show={showErrorModal} onHide={() => setShowErrorModal(false)}>
        <Modal.Header closeButton>
          <Modal.Title>Error</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <div className="error-modal-message">{error}</div>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={() => setShowErrorModal(false)}>
            Close
          </Button>
        </Modal.Footer>
      </Modal>
      <div className="custom-toast">
        <Toast onClose={() => setShowToast(false)} show={showToast} delay={5000} autohide>
          <Toast.Header>
            <strong className="me-auto">Notification</strong>
          </Toast.Header>
          <Toast.Body>{toastMessage}</Toast.Body>
        </Toast>
      </div>
    </div>
  );
}