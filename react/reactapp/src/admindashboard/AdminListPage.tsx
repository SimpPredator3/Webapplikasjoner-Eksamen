import React, { useState, useEffect } from 'react';
import PostTable from './PostTable';
import PostGrid from './PostGrid';
import { Spinner, Alert, Button, Container } from 'react-bootstrap';
import { API_URL } from '../apiConfig';
import { Post } from '../types/Post'; // Import the Post type

const PostListPage: React.FC = () => {
    const [posts, setPosts] = useState<Post[]>([]);
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);
    const [view, setView] = useState<"table" | "grid">("table");

    const fetchPosts = async () => {
        setLoading(true);
        setError(null);

        try {
            const response = await fetch(`${API_URL}/api/admindash`);
            if (!response.ok) {
                throw new Error('Failed to fetch posts');
            }
            const data: Post[] = await response.json();
            setPosts(data.slice(0, 20));
        } catch (err: any) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchPosts();
    }, []);

    const createPost = async (newPost: Post) => {
        try {
            const response = await fetch(`${API_URL}/api/admindash/create`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(newPost),
            });

            if (!response.ok) {
                throw new Error('Failed to create post');
            }

            const createdPost: Post = await response.json();
            setPosts((prevPosts) => [...prevPosts, createdPost]);
        } catch (err: any) {
            setError(err.message);
        }
    };

    const updatePost = async (updatedPost: Post) => {
        try {
            const response = await fetch(`${API_URL}/api/admindash/edit/${updatedPost.id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(updatedPost),
            });

            if (!response.ok) {
                throw new Error('Failed to update post');
            }

            setPosts((prevPosts) =>
                prevPosts.map((post) => (post.id === updatedPost.id ? updatedPost : post))
            );
        } catch (err: any) {
            setError(err.message);
        }
    };

    const deletePost = async (postId: number) => {
        try {
            const response = await fetch(`${API_URL}/api/admindash/delete/${postId}`, {
                method: 'DELETE',
            });

            if (!response.ok) {
                throw new Error('Failed to delete post');
            }

            setPosts((prevPosts) => prevPosts.filter((post) => post.id !== postId));
        } catch (err: any) {
            setError(err.message);
        }
    };

    const toggleView = () => {
        setView((prevView) => (prevView === "table" ? "grid" : "table"));
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

            )}
        </Container>
    /*         Koden streika når jeg putta inn dette, antar den fikser seg når jeg legger inn create, update og delete
         ? <PostTable posts={posts} createPost={createPost} updatePost={updatePost} deletePost={deletePost} />
         : <PostGrid posts={posts} createPost={createPost} updatePost={updatePost} deletePost={deletePost} /> */
    );
};

export default PostListPage;
