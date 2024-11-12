import React, { useState, useEffect } from 'react';
import PostTable from './PostTable';
import PostGrid from './PostGrid';
import { Spinner, Alert, Button, Container } from 'react-bootstrap';
import { API_URL } from '../apiConfig'; // Import your API_URL

const PostListPage = () => {
    const [posts, setPosts] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    const [view, setView] = useState("table"); // Initial view is set to "table"

    const fetchPosts = async () => {
        setLoading(true);
        setError(null);

        try {
            const response = await fetch(`${API_URL}/api/admindash`);
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

    const createPost = async (newPost) => {
        try {
            const response = await fetch(`${API_URL}/api/admindash/create`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    // Uncomment the following line if you have a token for authentication
                    // 'Authorization': `Bearer ${yourToken}`
                },
                body: JSON.stringify(newPost),
            });

            if (!response.ok) {
                throw new Error('Failed to create post');
            }

            const createdPost = await response.json();
            setPosts((prevPosts) => [...prevPosts, createdPost]); // Update posts state
        } catch (err) {
            setError(err.message);
        }
    };

    const updatePost = async (updatedPost) => {
        try {
            const response = await fetch(`${API_URL}/api/admindash/edit/${updatedPost.id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                    // Uncomment if you are using authentication
                    // 'Authorization': `Bearer ${yourToken}`
                },
                body: JSON.stringify(updatedPost),
            });

            if (!response.ok) {
                throw new Error('Failed to update post');
            }

            // Update the posts state with the updated post
            setPosts((prevPosts) =>
                prevPosts.map((post) => (post.id === updatedPost.id ? updatedPost : post))
            );
        } catch (err) {
            setError(err.message);
        }
    };

    const deletePost = async (postId) => {
        try {
            const response = await fetch(`${API_URL}/api/admindash/delete/${postId}`, {
                method: 'DELETE',
                headers: {
                    // Uncomment if you are using authentication
                    // 'Authorization': `Bearer ${yourToken}`
                },
            });

            if (!response.ok) {
                throw new Error('Failed to delete post');
            }

            // Remove the deleted post from state
            setPosts((prevPosts) => prevPosts.filter((post) => post.id !== postId));
        } catch (err) {
            setError(err.message);
        }
    };

    const toggleView = () => {
        setView(prevView => (prevView === "table" ? "grid" : "table"));
    };

    return (
        <Container className="mt-4">
            <div className="d-flex justify-content-between align-items-center mb-3">
                <h1 className="mb-0">Admin Dashboard</h1>
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
                view === "table" 
                    ? <PostTable posts={posts} createPost={createPost} updatePost={updatePost} deletePost={deletePost} /> 
                    : <PostGrid posts={posts} createPost={createPost} updatePost={updatePost} deletePost={deletePost} />
            )}
        </Container>
    );
};

export default PostListPage;
