import React, { useState, useEffect } from "react";
import { Button, Modal, Form, NavDropdown } from "react-bootstrap";
import { useUser } from '../components/UserContext';
import "./LoginModalComponent.css";
import '../App.css';

function LoginModalComponent() {
    // State for toggling login and register modals
    const [showLoginModal, setShowLoginModal] = useState(false);
    const [showRegisterModal, setShowRegisterModal] = useState(false);

    // States for user input fields
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [registerEmail, setRegisterEmail] = useState("");
    const [registerPassword, setRegisterPassword] = useState("");

    // State for error messages and logged-in user information
    const [error, setError] = useState("");
    const [userName, setUserName] = useState(null);

    const { refreshUserDetails, setUser } = useUser();

    // Fetch user identity
    const fetchUserIdentity = async () => {
        try {
            const response = await fetch("http://localhost:5141/api/auth/user", {
                method: "GET",
                credentials: "include",
            });
            if (response.ok) {
                const data = await response.json();
                setUserName(data.name);
            } else if (response.status === 401 || response.status === 404) {
                // Handle cases where the user is not logged in
                setUserName(null);
            } else {
                console.error("Unexpected response:", response.status);
            }
        } catch (error) {
            console.error("Error fetching user identity:", error);
            setUserName(null);
        }
    };

    useEffect(() => {
        fetchUserIdentity();
    }, []);

    // Handle user login
    const handleLogin = async (e) => {
        e.preventDefault();
        setError("");
        try {
            const response = await fetch("http://localhost:5141/api/auth/login", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                credentials: "include",
                body: JSON.stringify({ email, password, rememberMe: true }),
            });
            if (response.ok) {
                setShowLoginModal(false);
                fetchUserIdentity();
                refreshUserDetails(); // Update user details after login
            } else {
                const errorData = await response.json();
                setError("Login failed: " + (errorData.message || "Unknown error"));
            }
        } catch (error) {
            console.error("Error during login:", error);
            setError("An error occurred. Please try again.");
        }
    };

    // Handle user registration
    const handleRegister = async (e) => {
        e.preventDefault();
        setError("");
        console.log("Register button clicked");

        try {
            const response = await fetch("http://localhost:5141/api/auth/register", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ email: registerEmail, password: registerPassword }),
            });

            if (response.ok) {
                console.log("Registration successful");
                setShowRegisterModal(false);
                setShowLoginModal(true);
            } else {
                const errorData = await response.json();
                setError("Registration failed: " + (errorData.message || "Unknown error"));
            }
        } catch (error) {
            console.error("Error during registration:", error);
            setError("An error occurred. Please try again.");
        }
    };

    // Handle user logout
    const handleLogout = async () => {
        try {
            const response = await fetch("http://localhost:5141/api/auth/logout", {
                method: "POST",
                credentials: "include",
            });
            if (response.ok) {
                setUser(null);
                setUserName(null);
            } else {
                setError("Logout failed");
            }
        } catch (error) {
            console.error("Error during logout:", error);
            setError("An error occurred. Please try again.");
        }
    };

    return (
        <>
            <div className="top-right-corner">
                {userName ? (
                    <>
                        <span className="me-2">Welcome, {userName}</span>
                        <Button variant="outline-danger" onClick={handleLogout}>
                            Logout
                        </Button>
                    </>
                ) : (
                    <Button variant="outline-primary" onClick={() => setShowLoginModal(true)}>
                        Login
                    </Button>
                )}
            </div>

            {/* Add to Hamburger Menu for small screens */}
            <div className="d-lg-none">
                {userName ? (
                    <NavDropdown title="Account" id="user-nav-dropdown">
                        <NavDropdown.Item disabled>{userName}</NavDropdown.Item>
                        <NavDropdown.Divider />
                        <NavDropdown.Item>
                            <Button variant="outline-danger" onClick={handleLogout}>
                                Logout
                            </Button>
                        </NavDropdown.Item>
                    </NavDropdown>
                ) : (
                    <NavDropdown title="Account" id="guest-nav-dropdown">
                        <NavDropdown.Item>
                            <Button variant="outline-primary" onClick={() => setShowLoginModal(true)}>
                                Login
                            </Button>
                        </NavDropdown.Item>
                    </NavDropdown>
                )}
            </div>

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
                    <div className="text-center mt-3">
                        <button
                            type="button"
                            className="link-button"
                            onClick={() => {
                                console.log("Switching to Register modal"); // Debugging
                                setShowLoginModal(false);
                                setShowRegisterModal(true);
                            }}
                        >
                            Don't have an account? Register here
                        </button>
                    </div>
                </Modal.Body>
            </Modal>

            {/* Registration Modal */}
            <Modal show={showRegisterModal} onHide={() => setShowRegisterModal(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>Register</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form onSubmit={handleRegister}>
                        <Form.Group controlId="registerEmail">
                            <Form.Label>Email address</Form.Label>
                            <Form.Control
                                type="email"
                                placeholder="Enter email"
                                value={registerEmail}
                                onChange={(e) => setRegisterEmail(e.target.value)}
                                required
                            />
                        </Form.Group>
                        <Form.Group controlId="registerPassword">
                            <Form.Label>Password</Form.Label>
                            <Form.Control
                                type="password"
                                placeholder="Password"
                                value={registerPassword}
                                onChange={(e) => setRegisterPassword(e.target.value)}
                                required
                            />
                        </Form.Group>
                        {error && <p style={{ color: "red" }}>{error}</p>}
                        <Button variant="primary" type="submit" className="mt-3">
                            Register
                        </Button>
                    </Form>
                    <div className="text-center mt-3">
                        <button
                            type="button"
                            className="link-button"
                            onClick={() => {
                                console.log("Switching to Login modal"); // Debugging
                                setShowRegisterModal(false);
                                setShowLoginModal(true);
                            }}
                        >
                            Already have an account? Login here
                        </button>
                    </div>
                </Modal.Body>
            </Modal>
        </>
    );
}

export default LoginModalComponent;
