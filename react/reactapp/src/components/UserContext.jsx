import React, { createContext, useContext, useState, useEffect } from 'react';

const UserContext = createContext();

export const useUser = () => useContext(UserContext);

export const UserProvider = ({ children }) => {
    const [user, setUser] = useState(null);

    useEffect(() => {
        const fetchUserRole = async () => {
            try {
                const response = await fetch('/api/auth/user/role', {
                    credentials: 'include' // Include cookies in the request
                });
                if (response.ok) {
                    const { role } = await response.json();
                    console.log('Fetched user role:', role); // Debugging log
                    setUser({ role }); // Set only the role in the user state
                } else {
                    console.error('Failed to fetch user role');
                }
            } catch (error) {
                console.error('Error fetching user role:', error);
            }
        };

        fetchUserRole();
    }, []);

    return (
        <UserContext.Provider value={{ user, setUser }}>
            {children}
        </UserContext.Provider>
    );
};
