import React, { useState, useEffect } from 'react';
import PostTable from './PostTable';
import PostGrid from './PostGrid';
import { Spinner, Alert, Button, Container } from 'react-bootstrap';
import { API_URL } from '../apiConfig'; // Import API_URL from your config if you have it there

const PostListPage = () => {
    const [posts, setPosts] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    const [view, setView] = useState("table"); // Initial view is set to "table"

    const fetchPosts = async () => {
        setLoading(true);
        setError(null);

        try {
            const response = await fetch(`${API_URL}/api/post`);
            if (!response.ok) {
                throw new Error('Failed to fetch posts');
            }
            const data = await response.json();
            setPosts(data.slice(0, 20)); // Limit to 20 posts
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchPosts();
    }, []);

    const toggleView = () => {
        setView(prevView => (prevView === "table" ? "grid" : "table"));
    };

    return (
        <Container className="mt-4">
            <div className="d-flex justify-content-between align-items-center mb-3">
                <h1 className="mb-0">Posts</h1>
                <Button onClick={toggleView} variant="primary">
                    {view === "table" ? "Switch to Grid View" : "Switch to Table View"}
                </Button>
            </div>

            {loading && (
                <div className="text-center">
                    <Spinner animation="border" role="status">
                        <span className="sr-only">Loading...</span>
                    </Spinner>
                </div>
            )}
            {error && <Alert variant="danger">{error}</Alert>}
            {!loading && !error && (
                view === "table" ? <PostTable posts={posts} API_URL={API_URL} /> : <PostGrid posts={posts} API_URL={API_URL} />
            )}
        </Container>
    );
};

export default PostListPage;