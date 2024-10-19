import React from 'react';
import { Container, Row, Col, Button } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import SignupForm from '../components/SignUpForm';
import backgroundImage from '../images/bluebackground.png';

const backgroundStyle = {
  backgroundImage: `url(${backgroundImage})`,
  backgroundSize: 'cover',
  backgroundPosition: 'center',
  minHeight: '100vh',
  display: 'flex',
  flexDirection: 'column',
  justifyContent: 'center',
  alignItems: 'center',
};

function Signup() {
  return (
    <div style={backgroundStyle}>
      <Container>
        <Row className="justify-content-center mb-4">
          <Col xs={12} md={6} className="text-center">
            <Button as={Link} to='/' className="custom-button_brand_2 mb-4" style={{ fontSize: '24px', padding: '10px 20px' }}>
              TeachHub
            </Button>
          </Col>
        </Row>
        <Row className="justify-content-center">
          <Col xs={12} md={6}>
            <SignupForm />
          </Col>
        </Row>
      </Container>
    </div>
  );
}

export default Signup;