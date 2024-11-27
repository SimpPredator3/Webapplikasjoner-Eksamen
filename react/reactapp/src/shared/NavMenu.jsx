// NavMenu.js
import React, { useEffect, useState } from 'react';
import { Nav, Navbar } from 'react-bootstrap';
import { useUser } from '../components/UserContext';
import logo from '../assets/notehub.png';
import LoginModalComponent from '../components/LoginModalComponent';
import '../App.css';
import {useNavigate} from 'react-router-dom';

const NavMenu = ({ theme, toggleTheme }) => {
    const { user } = useUser();
    const [isAdmin, setIsAdmin] = useState(false);
    const navigate = useNavigate();

    const handleMyPosts = () => {
        navigate('/posts', { state: { view: 'MyPost' } });
    };


    useEffect(() => {
        setIsAdmin(user?.role === 'Admin'); // Update isAdmin whenever user role changes
    }, [user]);

    return (
        <Navbar expand="lg" className="navbar-container">
            <Navbar.Brand href="/">
                <img
                    src={logo}
                    alt="Notehub Logo"
                    style={{ width: '170px', height: '60px', marginRight: '5px' }}
                />
            </Navbar.Brand>
            <Navbar.Toggle aria-controls="basic-navbar-nav" />
            <Navbar.Collapse id="basic-navbar-nav">
                <Nav className="me-auto nav-custom">
                    <Nav.Link href="/">Home</Nav.Link>
                    <Nav.Link onClick={handleMyPosts} style={{ cursor: 'pointer' }}> 
                        My Posts
                    </Nav.Link>
                    {isAdmin && (
                        <Nav.Link href="/AdminDash">Admin Dashboard</Nav.Link>
                    )}
                </Nav>
                <Nav className="ms-auto">
                    {/* Dark Mode Toggle Button */}
                    <Nav.Item className="d-flex align-items-center">
                        <button
                            className="darkmode-btn btn btn-outline-secondary me-3"
                            onClick={toggleTheme}
                        >
                            {theme === "dark" ? (
                                <i className="fas fa-sun"></i> /* Sun icon for light mode */
                            ) : (
                                <i className="fas fa-moon"></i> /* Moon icon for dark mode */
                            )}
                        </button>
                    </Nav.Item>
                    {/* Login Modal Component */}
                    <LoginModalComponent />
                </Nav>
            </Navbar.Collapse>
        </Navbar>
    );
};

export default NavMenu;
