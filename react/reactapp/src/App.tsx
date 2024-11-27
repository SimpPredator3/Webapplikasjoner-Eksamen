import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom';
import Container from 'react-bootstrap/Container';
import HomePage from './home/HomePage';
import NavMenu from './shared/NavMenu';
import PostListPage from './posts/PostListPage';
import AdminListPage from './admindashboard/AdminListPage';
import { UserProvider } from './components/UserContext';
import PostCreatePage from './posts/PostCreatePage';
import PostEditPage from './posts/PostEditPage';
import './App.css';
import ErrorBoundary from "./error-handling/ErrorBoundary.js";




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
    <ErrorBoundary>
    <UserProvider>
      <Router>
        <Container>
          <NavMenu theme={theme} toggleTheme={toggleTheme} />
          <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/posts" element={<PostListPage />} />
            <Route path="/AdminDash" element={<AdminListPage />} />
            <Route path="/postcreate" element={<PostCreatePage />} />
            <Route path="/post/edit/:id" element={<PostEditPage />} />
            <Route path="*" element={<Navigate to="/" replace />} />
          </Routes>
        </Container>
      </Router>
    </UserProvider>
    </ErrorBoundary>
  );
}

export default App;