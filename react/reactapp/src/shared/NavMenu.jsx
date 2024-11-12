// NavMenu.js
import React, { useEffect, useState } from 'react';
import { Nav, Navbar, NavDropdown } from 'react-bootstrap';
import { useUser } from '../components/UserContext';

const NavMenu = () => {
    const { user } = useUser();
    const [isAdmin, setIsAdmin] = useState(false);
    

    useEffect(() => {
        setIsAdmin(user?.role === 'Admin'); // Update isAdmin whenever user role changes
    }, [user]);

    return (
        <Navbar expand="lg">
            <Navbar.Brand href="/">Pinstagram</Navbar.Brand>
            <Navbar.Toggle aria-controls="basic-navbar-nav" />
            <Navbar.Collapse id="basic-navbar-nav">
                <Nav className="me-auto">
                    <Nav.Link href="/">Home</Nav.Link>
                    <NavDropdown title="Dropdown" id="basic-nav-dropdown">
                        <NavDropdown.Item href="#action/3.1">Action</NavDropdown.Item>
                        <NavDropdown.Item href="#action/3.2">Another action</NavDropdown.Item>
                        <NavDropdown.Divider />
                        <NavDropdown.Item href="#action/3.4">Separated link</NavDropdown.Item>
                    </NavDropdown>
                    {isAdmin && (
                        <Nav.Link href="/AdminDash">Admin Dashboard</Nav.Link>
                    )}
                </Nav>
            </Navbar.Collapse>
        </Navbar>
    );
};

export default NavMenu;
