import React from 'react';
import { Button } from 'react-bootstrap';
import { ArrowLeft } from 'lucide-react';
import { useNavigate } from 'react-router-dom';

const BackToDashboardButton = () => {
  const navigate = useNavigate();

  const handleClick = () => {
    navigate('/Dashboard');
  };

  return (
    <Button
      variant="outline-primary"
      onClick={handleClick}
      className="position-absolute top-0 start-0 m-3"
      style={{ zIndex: 1000 }}
    >
      <ArrowLeft size={18} className="me-2" />
      Back to Dashboard
    </Button>
  );
};

export default BackToDashboardButton;