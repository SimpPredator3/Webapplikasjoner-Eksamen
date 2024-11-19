import React, { createContext, useContext, useState, useEffect } from 'react';

const UserContext = createContext();

export const useUser = () => useContext(UserContext);

export const UserProvider = ({ children }) => {
    const [user, setUser] = useState(null);

    // Function to fetch user role
    const fetchUserRole = async () => {
        try {
            const response = await fetch('http://localhost:5141/api/auth/user/role', {
                credentials: 'include'
            });
            if (response.ok) {
                const { role } = await response.json();
                console.log('Fetched user role:', role); // Debugging log
                setUser({ role }); // Set only the role in the user state
            } else {
                console.error('Failed to fetch user role:', response.status, response.statusText);
            }
        } catch (error) {
            console.error('Error fetching user role:', error);
        }
    };

    // Trigger fetchUserRole on component mount
    useEffect(() => {
        fetchUserRole();
    }, []);

    
    const refreshUserRole = fetchUserRole;

    return (
        <UserContext.Provider value={{ user, setUser, refreshUserRole }}>
            {children}
        </UserContext.Provider>
    );
};
