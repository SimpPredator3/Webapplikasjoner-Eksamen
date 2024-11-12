import React from 'react';
import PostListPage from '../posts/PostListPage';

const HomePage = () => {
    return (
        <div className="container mt-5">
            <h1 className="text-center mb-4">Welcome to Pinstagram!</h1>
            <p className="text-center">Your place to share and view photos!</p>
            {/* Render the PostListPage component here */}
            <PostListPage view="grid" /> {/* Or set view="table" */}
        </div>
    );
};

export default HomePage;