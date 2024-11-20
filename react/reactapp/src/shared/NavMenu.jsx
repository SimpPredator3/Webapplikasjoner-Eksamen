// NavMenu.js
import React, { useEffect, useState } from 'react';
import { Nav, Navbar } from 'react-bootstrap';
import { useUser } from '../components/UserContext';
import logo from '../assets/notehub.png';
import LoginModalComponent from '../components/LoginModalComponent';

const NavMenu = ({ theme, toggleTheme }) => {
    const { user } = useUser();
    const [isAdmin, setIsAdmin] = useState(false);
    

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
                <Nav className="me-auto">
                    <Nav.Link href="/">Home</Nav.Link>
                    {isAdmin && (
                        <Nav.Link href="/AdminDash">Admin Dashboard</Nav.Link>
                    )}
                </Nav>
                {/* Dark Mode Toggle Button */}
                <div className="top-right-corner">
                    <button className="darkmode-btn" onClick={toggleTheme}>
                        {theme === 'dark' ? (
                            <i className="fas fa-sun"></i> /* Sun icon for light mode */
                        ) : (
                            <i className="fas fa-moon"></i> /* Moon icon for dark mode */
                        )}
                    </button>
                    <LoginModalComponent />
                </div>
            </Navbar.Collapse>
        </Navbar>
    );
};

export default NavMenu;
