import React, { useState, useEffect, useContext } from 'react';
import PostGrid from './PostGrid';
import { Spinner, Alert, Button, Container } from 'react-bootstrap';
import { API_URL } from '../apiConfig';
import { Post } from '../types/Post';
import './PostListPage.css';
import PostList from './PostList';


interface PostListPageProps {
    initialView?: "list" | "grid"; // Optional prop for initial view
    lockedView?: "list" | "grid";  // Optional prop to lock the view
}

const PostListPage: React.FC<PostListPageProps> = ({ initialView = "grid", lockedView }) => {
    const [posts, setPosts] = useState<Post[]>([]);
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);
    const [view, setView] = useState<"list" | "grid">(lockedView ?? initialView);

    const fetchPosts = async () => {
        setLoading(true);
        setError(null);

        try {
            const response = await fetch(`${API_URL}/api/post`);
            if (!response.ok) {
                throw new Error('Failed to fetch posts');
            }
            const data: Post[] = await response.json();
            setPosts(data);
        } catch (err: any) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };


    useEffect(() => {
        fetchPosts();
    }, []);

    const toggleToGrid = () => setView("grid");
    const toggleToList = () => setView("list");

    const handleDeletePost = async (id: number) => {
        try {
            const response = await fetch(`${API_URL}/api/post/${id}`, {
                method: 'DELETE',
            });

            if (!response.ok) {
                throw new Error('Failed to delete post');
            }

            // Remove the deleted post from the state
            setPosts(posts.filter(post => post.id !== id));
        } catch (err) {
            console.error(err.message);
            setError('Failed to delete the post.');
        }
    };

    const handleEditPost = async (id: number, updatedPost: Partial<Post>) => {
        try {
            const response = await fetch(`${API_URL}/api/post/${id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(updatedPost),
            });

            if (!response.ok) {
                throw new Error('Failed to update post');
            }

            const updatedData = await response.json();

            // Update the local state with the updated post
            setPosts((prevPosts) =>
                prevPosts.map((post) => (post.id === id ? { ...post, ...updatedData } : post))
            );
        } catch (error) {
            console.error(error);
        }
    };

    return (
        <Container className="mt-4">
            <div className="d-flex justify-content-between align-items-center mb-3">
                <h1 className="mb-0">Posts</h1>
                <Button href='/postcreate' className='btn btn-secondary mt-3'>Create New Post</Button>
                {!lockedView && (
                    <div className="d-flex">
                        <button
                            onClick={toggleToGrid}
                            className={`btn me-2 ${view === "grid" ? "active-btn" : "inactive-btn"}`}
                            title="Grid View"
                        >
                            <i className="fas fa-th"></i>
                        </button>
                        <button
                            onClick={toggleToList}
                            className={`btn ${view === "list" ? "active-btn" : "inactive-btn"}`}
                            title="List View"
                        >
                            <i className="fas fa-list"></i>
                        </button>
                    </div>
                )}
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
                (lockedView ?? view) === "list"
                    ? <PostList posts={posts} API_URL={API_URL} onDelete={handleDeletePost} />
                    : <PostGrid posts={posts} API_URL={API_URL} onDelete={handleDeletePost} />
            )}
        </Container>
    );
};

export default PostListPage;
