import React, { useState } from 'react';
import { Nav, Navbar, NavDropdown, Modal, Button, Form } from 'react-bootstrap';

const NavMenu = () => {
    const [showLoginModal, setShowLoginModal] = useState(false);
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const handleLogin = async (e) => {
        e.preventDefault();
        
        try {
            const response = await fetch('http://localhost:3000/api/auth/login', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ email, password }),
                credentials: 'include'
            });

             // Log the response status and body
            console.log('Response Status:', response.status);
            const responseBody = await response.text();
            console.log('Response Body:', responseBody);

            // Check if the response is OK (status code 200)
            if (!response.ok) {
                // Attempt to parse the responseBody as JSON if not OK
                try {
                    const errorData = JSON.parse(responseBody);
                    throw new Error(errorData.Message || 'Login failed');
                } catch (jsonError) {
                    throw new Error('Login failed with non-JSON response: ' + responseBody);
                }
            }

            const data = JSON.parse(responseBody); // This assumes response is JSON
            console.log(data.Message); // "Login successful"
            setShowLoginModal(false); // Close modal on success
        } catch (error) {
            console.error('Login error:', error);
            alert(error.message);
        }
};

    return (
        <>
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
                        <Nav.Link href="/AdminDash">Admin Dashboard</Nav.Link>
                    </Nav>
                    <Button variant="outline-primary" onClick={() => setShowLoginModal(true)}>
                        Login
                    </Button>
                </Navbar.Collapse>
            </Navbar>

            {/* Login Modal */}
            <Modal show={showLoginModal} onHide={() => setShowLoginModal(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>Login</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form onSubmit={handleLogin}>
                        <Form.Group controlId="formBasicEmail">
                            <Form.Label>Email address</Form.Label>
                            <Form.Control
                                type="email"
                                placeholder="Enter email"
                                value={email}
                                onChange={(e) => setEmail(e.target.value)}
                                required
                            />
                        </Form.Group>

                        <Form.Group controlId="formBasicPassword">
                            <Form.Label>Password</Form.Label>
                            <Form.Control
                                type="password"
                                placeholder="Password"
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                                required
                            />
                        </Form.Group>

                        <Button variant="primary" type="submit" className="mt-3">
                            Login
                        </Button>
                    </Form>
                </Modal.Body>
            </Modal>
        </>
    );
};

export default NavMenu;
