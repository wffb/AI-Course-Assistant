import React from 'react';
import { Container, Row, Col, Card, Button } from 'react-bootstrap';
import { Book, Users, Monitor, BookOpen, FileText, Settings } from 'lucide-react';
import LogoutButton from '../components/LogOut';
export default function AdminDashboard() {
  const dashboardItems = [
    { title: 'Student Management', icon: Book, link: '/student-management' },
    { title: 'AI assistant Information', icon: Users, link: '/Dashboard/assistant_manage' },
    { title: 'Course', icon: BookOpen, link: '/Dashboard/course' },
    // { title: 'Files', icon: FileText, link: '/files' },
  ];

  return (
    <Container className="d-flex flex-column justify-content-center" style={{ minHeight: '100vh' }}>
      <LogoutButton />
      <h1 className="text-center mb-5">Teacher Admin Dashboard</h1>
      <Row className="justify-content-center">
        {dashboardItems.map((item, index) => (
          <Col key={index} md={4} className="mb-4">
            <Card className="h-100">
              <Card.Body className="d-flex flex-column align-items-center justify-content-center">
                <item.icon size={48} className="mb-3" />
                <Card.Title>{item.title}</Card.Title>
                <Button variant="primary" href={item.link} className="mt-3">
                  View
                </Button>
              </Card.Body>
            </Card>
          </Col>
        ))}
      </Row>
    </Container>
  );
}