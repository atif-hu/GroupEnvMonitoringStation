import React, { useState, useEffect } from 'react';

import Router from 'src/routes/sections'

import { SignupView } from 'src/sections/signup';

const AuthPage = () => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);

    useEffect(() => {
        const checkAuth = async () => {
            try {
                let token='';
                if(document.cookie!==''){
                    token = document.cookie.split('; ').find(row => row.startsWith('access_token')).split('=')[1];
                }

                const response = await fetch('https://localhost:7132/Auth', {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${token}`
                    }
                });
                if (response.ok) {
                    const data = await response.json();
                    setIsAuthenticated(data.isAuthenticated);
                } else {
                    throw new Error('Error checking authentication');
                }
            } catch (error) {
                console.error('Error checking authentication:', error);
            }
        };

        checkAuth();
    }, []);

    return isAuthenticated ? <Router/> : <SignupView />;
};

 
export default AuthPage;
