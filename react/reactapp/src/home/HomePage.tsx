import React from 'react';
import PostListPage from '../posts/PostListPage';

const HomePage: React.FC = () => {
    return (
        <div className="container mt-5">
            <h1 className="text-center mb-4">Welcome to NoteHub!</h1>
            <p className="text-center intro-text">Overwhelmed with assignments, projects, or just love sharing knowledge? <br></br> NoteHub simplifies your life by organizing, sharing, and collaborating on notes and materials—all in one place!</p>
            {/* Render the PostListPage component with lockedView set to "grid" */}
            <PostListPage initialView="grid" />
        </div>
    );
};

export default HomePage;
