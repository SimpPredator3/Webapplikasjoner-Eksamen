import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom';
import Container from 'react-bootstrap/Container';
import HomePage from './home/HomePage';
import NavMenu from './shared/NavMenu';
import PostListPage from './posts/PostListPage';
import AdminListPage from './admindashboard/AdminListPage';
import LoginModalComponent from "./components/LoginModalComponent";
import { UserProvider } from './components/UserContext';
import PostCreatePage from './posts/PostCreatePage';
import './App.css';


const App: React.FC = () => {
  // State to track the current theme
  const [theme, setTheme] = useState('light');

  // Function to toggle the theme
  const toggleTheme = () => {
    const newTheme = theme === 'dark' ? 'light' : 'dark';
    setTheme(newTheme);
    document.body.setAttribute('data-bs-theme', newTheme);
    localStorage.setItem('theme', newTheme);
  };

  useEffect(() => {
    const savedTheme = localStorage.getItem('theme') || 'light';
    setTheme(savedTheme);
    document.body.setAttribute('data-bs-theme', savedTheme);
  }, []);



  return (
    <UserProvider>
      <Router>
        <Container>
          <NavMenu />

          {/* Dark Mode Toggle Button */}
          <div className="d-none d-lg-block">
            <button className="darkmode-btn" onClick={toggleTheme}>
              {theme === 'dark' ? (
                <i className="fas fa-sun"></i> /* Sun icon for light mode */
              ) : (
                <i className="fas fa-moon"></i> /* Moon icon for dark mode */
              )}
            </button>
          </div>
          <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/posts" element={<PostListPage />} />
            <Route path="/AdminDash" element={<AdminListPage />} />
            <Route path="/postcreate" element={<PostCreatePage />} />
            <Route path="*" element={<Navigate to="/" replace />} />
          </Routes>
        </Container>
      </Router>
    </UserProvider>
  );
}

export default App;