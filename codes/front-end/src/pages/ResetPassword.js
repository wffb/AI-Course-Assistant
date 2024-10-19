import React, { useState } from 'react';
import { Button, Form, Card, Alert } from 'react-bootstrap';

function PasswordResetPage() {
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [submitted, setSubmitted] = useState(false);
    const [error, setError] = useState('');
    const backendUrl = process.env.REACT_APP_BACKEND_URL;
    const handleUsernameChange = (event) => {
        setUsername(event.target.value);
    };

    const handleEmailChange = (event) => {
        setEmail(event.target.value);
    };

    const resetPassword = async () => {
        try {
            const response = await fetch(backendUrl+'/user/reset-password/', {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ username, email }),
            });

            if (response.status === 201) {
                console.log("Reset email sent. Please check your inbox.");
                setSubmitted(true);
                setError('');
                return true;

            } 
            else if(response.status === 402) {
                setError("Email address not found.");
                setSubmitted(false)
            }
            else {
                const result = await response.json();
                throw new Error(result.error || "Sign up failed, please try again later.");
            }
        } catch (error) {
            setError(error.message);
            setSubmitted(false)
            return false;
        }
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        if (!email || !username) {
            setError('Please enter both username and your email address.');
            return;
        }
        await resetPassword();
    };

    return (
        <div className="d-flex justify-content-center align-items-center" style={{ height: '100vh' }}>
            <Card style={{ width: '400px' }}>
                <Card.Body>
                    <Card.Title>Reset Your Password</Card.Title>
                    <Form onSubmit={handleSubmit}>
                        <Form.Group className="mb-3">
                            <Form.Label>Username</Form.Label>
                            <Form.Control
                                type="text"
                                placeholder="Enter your username"
                                value={username}
                                onChange={handleUsernameChange}
                                required
                            />
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Label>Email address</Form.Label>
                            <Form.Control
                                type="email"
                                placeholder="Enter your email"
                                value={email}
                                onChange={handleEmailChange}
                                required
                            />
                        </Form.Group>
                        <Button variant="primary" type="submit" className="w-100">
                            Send Reset Link
                        </Button>
                    </Form>
                    {submitted && (
                        <Alert variant="success" className="mt-3">
                            Check your email for the reset link.
                        </Alert>
                    )}
                    {error && (
                        <Alert variant="danger" className="mt-3">
                            {error}
                        </Alert>
                    )}
                </Card.Body>
            </Card>
        </div>
    );
}

export default PasswordResetPage;
