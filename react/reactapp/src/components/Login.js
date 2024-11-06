// src/components/Login.js
import React, { useState } from "react";

function Login() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");

    const handleLogin = async (e) => {
        e.preventDefault();

        try {
            const response = await fetch("http://localhost:5141/api/auth/login", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                credentials: "include", // Include cookies for session
                body: JSON.stringify({ email, password, rememberMe: true }),
            });

            if (response.ok) {
                const data = await response.json();
                console.log("Login successful:", data.message);
            } else {
                const errorData = await response.json();
                setError("Login failed: " + errorData.message);
            }
        } catch (error) {
            console.error("Login failed with error:", error);
            const errorResponse = await error.response?.json();
            setError("An error occurred: " + (errorResponse?.message || error.message));
        }
    };

    return (
        <div>
            <h2>Login</h2>
            <form onSubmit={handleLogin}>
                <input
                    type="email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    placeholder="Email"
                    required
                />
                <input
                    type="password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    placeholder="Password"
                    required
                />
                <button type="submit">Login</button>
            </form>
            {error && <p style={{ color: "red" }}>{error}</p>}
        </div>
    );
}

export default Login;
