import React, { useState } from "react";
import { Button, Modal, Form, Navbar } from "react-bootstrap";

function LoginModalComponent() {
    const [showLoginModal, setShowLoginModal] = useState(false);
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");

    const handleLogin = async (e) => {
        e.preventDefault();
        setError(""); // Reset error on new login attempt

        try {
            const response = await fetch("http://localhost:5141/api/auth/login", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                credentials: "include",
                body: JSON.stringify({ email, password, rememberMe: true }),
            });

            if (response.ok) {
                const data = await response.json();
                console.log("Login successful:", data.message);
                setShowLoginModal(false); // Close modal on successful login
                // Add any additional logic, like updating user state
            } else {
                const errorData = await response.json();
                setError("Login failed: " + (errorData.message || "Unknown error"));
            }
        } catch (error) {
            console.error("Error during login:", error);
            setError("An error occurred. Please try again.");
        }
    };

    return (
        <>
            <Navbar>
                <Navbar.Collapse className="justify-content-end">
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

                        {error && <p style={{ color: "red" }}>{error}</p>}

                        <Button variant="primary" type="submit" className="mt-3">
                            Login
                        </Button>
                    </Form>
                </Modal.Body>
            </Modal>
        </>
    );
}

export default LoginModalComponent;
