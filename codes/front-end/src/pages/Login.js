import React from 'react';
import { Container, Row, Col, Button } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import LoginForm from '../components/LoginForm';
import backgroundImage from '../images/bluebackground.png';
export default function Login() {
  const backgroundStyle = {
    backgroundImage: `url(${backgroundImage})`,
    backgroundSize: 'cover',
    backgroundPosition: 'center',
    minHeight: '100vh',
  };

  return (
    <div style={backgroundStyle} className="d-flex flex-column justify-content-center align-items-center">
      <Container className="mb-4">
        <Row className="justify-content-center">
          <Col xs={12} md={6} className="text-center">
            <Button as={Link} to='/' className="custom-button_brand_2 mb-4" style={{ fontSize: '24px', padding: '10px 20px' }}>
              TeachHub
            </Button>
          </Col>
        </Row>
      </Container>
      <Container>
        <Row className="justify-content-center">
          <Col xs={12} md={6}>
            <LoginForm />
          </Col>
        </Row>
      </Container>
    </div>
  );
}




