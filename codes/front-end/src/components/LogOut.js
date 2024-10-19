import React from 'react';
import { Button } from 'react-bootstrap';
import { LogOut } from 'lucide-react';
import { useNavigate } from 'react-router-dom';

const LogoutButton = () => {
  const navigate = useNavigate();

  const handleLogout = () => {
    // Remove the token from localStorage
    localStorage.removeItem('token');
    
    // Redirect to the login page
    navigate('/login');
  };

  return (
    <Button
      variant="outline-danger"
      onClick={handleLogout}
      className="position-absolute top-0 end-0 m-3"
      style={{ zIndex: 1000 }}
    >
      <LogOut size={18} className="me-2" />
      Log out
    </Button>
  );
};

export default LogoutButton;