import React, { useState, useEffect } from "react";
import { Button, Modal, Form, Navbar } from "react-bootstrap";
import "./LoginModalComponent.css";

function LoginModalComponent() {
    const [showLoginModal, setShowLoginModal] = useState(false);
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");
    const [userName, setUserName] = useState(null);


     // Function to fetch the user's identity from the backend
     const fetchUserIdentity = async () => {
        try {
            const response = await fetch("http://localhost:5141/api/auth/user", {
                method: "GET",
                credentials: "include",
            });
            if (response.ok) {
                const data = await response.json();
                setUserName(data.name); // Set the user name if authenticated
            } else {
                setUserName(null); // Clear name if not authenticated
            }
        } catch (error) {
            console.error("Error fetching user identity:", error);
            setUserName(null);
        }
    };

    // Call fetchUserIdentity on component mount to check if the user is already logged in
    useEffect(() => {
        fetchUserIdentity();
    }, []);


    // Login function
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
                fetchUserIdentity(); // Fetch the user's identity after login
            } else {
                const errorData = await response.json();
                setError("Login failed: " + (errorData.message || "Unknown error"));
            }
        } catch (error) {
            console.error("Error during login:", error);
            setError("An error occurred. Please try again.");
        }
    };

      // Logout function
      const handleLogout = async () => {
        try {
            const response = await fetch("http://localhost:5141/api/auth/logout", {
                method: "POST",
                credentials: "include",
            });
            if (response.ok) {
                setUserName(null); // Clear the user name on logout
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
            {/* Top-right login/logout button */}
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
