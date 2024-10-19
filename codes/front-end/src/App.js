import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { UserProvider } from './usercontext';
import Login from './pages/Login'; 
import Signup from './pages/SignUp';
import Dashboard from './pages/Dashboard'
import CustomerProfile from './pages/profile';
import PasswordResetPage from './pages/ResetPassword';
import Student_logs from './pages/student-manage';
import FileUploadPage from './pages/FileUpload';
import AIAssistantManagementPage from './pages/Ai_assistant';
import CourseManagementPage from './pages/Course';
function App() {
  return (
    <Router>
      {/* <UserProvider>  */}
        <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/login" element={<Login />} /> 
        <Route path="/signup" element={<Signup />} />
        <Route path="/Dashboard" element = {<Dashboard/>} />
        <Route path="/login/resetPassword" element={<PasswordResetPage />} />
        <Route path="/profile" element={<CustomerProfile />} />
        <Route path="/student-management" element={<Student_logs />} />
        {/* <Route path="/files" element = {<FileUploadPage/>}/> */}
        <Route path= "/Dashboard/assistant_manage" element = {<AIAssistantManagementPage/>}/>
        <Route path= "/Dashboard/course" element = {<CourseManagementPage/>}/>
        </Routes>
      {/* </UserProvider> */}
    </Router>
  );
}

export default App;


