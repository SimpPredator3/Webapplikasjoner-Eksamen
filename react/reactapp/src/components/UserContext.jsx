import React, { createContext, useContext, useState, useEffect } from 'react';

const UserContext = createContext();

export const useUser = () => useContext(UserContext);

export const UserProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true)

    // Function to fetch user details from the backend API
    const fetchUserDetails = async () => {
        setLoading(true);
        try {
            const response = await fetch('http://localhost:5141/api/auth/user/details', {
                credentials: 'include'
            });
            if (response.ok) {
                const userDetails = await response.json();
                console.log('Fetched user details:', userDetails); // Debugging log
                setUser(userDetails); // Set username, email, and role in the user state
            } else if (response.status === 401 || response.status === 404) {
                // User not authenticated or resource not found, clear user details
                setUser(null);
            } else {
                console.error('Failed to fetch user details:', response.status, response.statusText);
                setUser(null);
            }
        } catch (error) {
            console.error('Error fetching user details:', error);
            setUser(null);
        }finally {
            setLoading(false); // Ensure loading state is updated
        }
    };

    // Trigger fetchUserDetails on component mount
    useEffect(() => {
        fetchUserDetails();
    }, []);

    const refreshUserDetails = fetchUserDetails;

    return (
        <UserContext.Provider value={{ user, setUser, refreshUserDetails, loading }}>
            {children}
        </UserContext.Provider>
    );
};