import React, { useState } from 'react';
import { Card, Form, Button, Toast } from 'react-bootstrap';
import { Link } from 'react-router-dom';

const backendUrl = "http://localhost:8080";

export default function SignupForm() {
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    username: '',
    password: '',
    passwordConfirm: '',
    email: ''
  });
  const [errors, setErrors] = useState({});
  const [showSuccessToast, setShowSuccessToast] = useState(false);
  const [showErrorToast, setShowErrorToast] = useState(false);
  const [apiErrorMessage, setApiErrorMessage] = useState('');

  const handleInputChange = (event) => {
    const { id, value } = event.target;
    setFormData(prevData => ({
      ...prevData,
      [id]: value
    }));
  };

  const validateForm = () => {
    const newErrors = {};
    
    Object.keys(formData).forEach(key => {
      if (!formData[key].trim()) {
        newErrors[key] = 'This field is required';
      }
    });

    if (formData.username !== formData.username.toLowerCase()) {
      newErrors.username = 'Username must be in lowercase';
    }

    if (formData.password !== formData.passwordConfirm) {
      newErrors.passwordConfirm = 'Passwords do not match';
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleFormSubmit = async (e) => {
    e.preventDefault();
    if (validateForm()) {
      try {
        const response = await fetch(`${backendUrl}/register`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            username: formData.username,
            password: formData.password,
            isTeacher: true
          }),
        });

        const data = await response.json();

        if (data.code === 200) {
          setShowSuccessToast(true);
          setFormData({
            firstName: '',
            lastName: '',
            username: '',
            password: '',
            passwordConfirm: '',
            email: ''
          });
        } else {
          throw new Error(data.message || 'Registration failed');
        }
      } catch (error) {
        setApiErrorMessage(error.message);
        setShowErrorToast(true);
      }
    }
  };

  return (
    <Card className="p-4 custom-card2">
      <Card.Header className="text-center custom-cardheader" style={{ borderBottom: 'none' }}>
        Create your account
      </Card.Header>
      <Card.Body>
        <Form onSubmit={handleFormSubmit}>
          <div className="d-flex mb-1">
            <Form.Group controlId="firstName" className="flex-fill me-2">
              <Form.Label>First Name</Form.Label>
              <Form.Control
                type="text"
                placeholder="Enter your first name"
                maxLength="50"
                value={formData.firstName}
                onChange={handleInputChange}
                isInvalid={!!errors.firstName}
              />
              <Form.Control.Feedback type="invalid">{errors.firstName}</Form.Control.Feedback>
            </Form.Group>
            <Form.Group controlId="lastName" className="flex-fill ms-2">
              <Form.Label>Last Name</Form.Label>
              <Form.Control
                type="text"
                placeholder="Enter your last name"
                maxLength="50"
                value={formData.lastName}
                onChange={handleInputChange}
                isInvalid={!!errors.lastName}
              />
              <Form.Control.Feedback type="invalid">{errors.lastName}</Form.Control.Feedback>
            </Form.Group>
          </div>
          <Form.Group className="mb-3" controlId="username">
            <Form.Label style={{marginTop: "10px"}}>Username</Form.Label>
            <Form.Control
              type="text"
              placeholder="Enter username in lower case"
              maxLength="30"
              value={formData.username}
              onChange={handleInputChange}
              isInvalid={!!errors.username}
            />
            <Form.Control.Feedback type="invalid">{errors.username}</Form.Control.Feedback>
          </Form.Group> 
          <Form.Group className="mb-3" controlId="password">
            <Form.Label>Password</Form.Label>
            <Form.Control
              type="password"
              placeholder="Enter password"
              value={formData.password}
              onChange={handleInputChange}
              isInvalid={!!errors.password}
            />
            <Form.Control.Feedback type="invalid">{errors.password}</Form.Control.Feedback>
          </Form.Group>
          <Form.Group className="mb-3" controlId="passwordConfirm">
            <Form.Label>Confirm Password</Form.Label>
            <Form.Control
              type="password"
              placeholder="Confirm your password"
              value={formData.passwordConfirm}
              onChange={handleInputChange}
              isInvalid={!!errors.passwordConfirm}
            />
            <Form.Control.Feedback type="invalid">{errors.passwordConfirm}</Form.Control.Feedback>
          </Form.Group>
          <Form.Group className="mb-3" controlId="email">
            <Form.Label>Email</Form.Label>
            <Form.Control
              type="email"
              placeholder="Enter your email address"
              value={formData.email}
              onChange={handleInputChange}
              isInvalid={!!errors.email}
            />
            <Form.Control.Feedback type="invalid">{errors.email}</Form.Control.Feedback>
          </Form.Group>
          <Button variant="primary" type="submit" className="w-100 custom-button">
            Sign Up Now
          </Button>
          <div className="mt-3 d-flex align-items-center justify-content-center">
            <span className="mr-2">Already have an account?</span>
            <Button as={Link} to="/login" variant="link" style={{ textDecoration: 'underline' }}>
              Log in
            </Button>
          </div>
        </Form>
        <Toast
          show={showSuccessToast}
          onClose={() => setShowSuccessToast(false)}
          delay={5000}
          autohide
          className="custom-toast"
        >
          <Toast.Header closeButton={true}>
            <strong className="me-auto">Success</strong>
          </Toast.Header>
          <Toast.Body>
            Signup successful!<br/>
            The user registration is successful, please complete the personal information as soon as possible.
          </Toast.Body>
        </Toast>
        <Toast
          show={showErrorToast}
          onClose={() => setShowErrorToast(false)}
          delay={5000}
          autohide
          className="custom-toast"
        >
          <Toast.Header closeButton={true}>
            <strong className="me-auto">Error</strong>
          </Toast.Header>
          <Toast.Body>
            {apiErrorMessage}
          </Toast.Body>
        </Toast>
      </Card.Body>
    </Card>
  );
}