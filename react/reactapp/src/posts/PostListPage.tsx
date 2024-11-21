import React, { useState, useEffect } from 'react';
import PostGrid from './PostGrid';
import { Spinner, Alert, Button, Container, Modal } from 'react-bootstrap';
import { API_URL } from '../apiConfig';
import { Post } from '../types/Post';
import './PostListPage.css';
import PostList from './PostList';
import { useNavigate } from 'react-router-dom';
import { useUser } from '../components/UserContext';

interface PostListPageProps {
    initialView?: "list" | "grid"; // Optional prop for initial view
    lockedView?: "list" | "grid";  // Optional prop to lock the view
}

const PostListPage: React.FC<PostListPageProps> = ({ initialView = "grid", lockedView }) => {
    const [posts, setPosts] = useState<Post[]>([]);
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);
    const [view, setView] = useState<"list" | "grid">(lockedView ?? initialView);
    const [showModal, setShowModal] = useState<boolean>(false);
    const [postToDelete, setPostToDelete] = useState<number | null>(null);
    const navigate = useNavigate();
    const { user } = useUser();

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
    }, [user]); // Re-run fetchPosts whenever the user changes

    useEffect(() => {
        if (user) {
            setView(view); // Trigger a re-render when the user changes
        }
    }, [user]);

    const toggleToGrid = () => setView("grid");
    const toggleToList = () => setView("list");

    const confirmDeletePost = (id: number) => {
        setPostToDelete(id);
        setShowModal(true);
    };

    const cancelDelete = () => {
        setPostToDelete(null);
        setShowModal(false);
    };

    const handleDeletePost = async () => {
        if (postToDelete === null) return;

        try {
            const response = await fetch(`${API_URL}/api/post/${postToDelete}`, {
                method: 'DELETE',
            });

            if (!response.ok) {
                throw new Error('Failed to delete post');
            }

            setPosts(posts.filter(post => post.id !== postToDelete)); // Update the state
            setShowModal(false); // Close the modal
            setPostToDelete(null); // Clear the post to delete

            // Redirect to homepage
            navigate('/');
        } catch (err) {
            console.error(err.message);
            setError('Failed to delete the post.');
        }
    };

    const handleUpvote = async (postId: number) => {
        try {
            const response = await fetch(`${API_URL}/api/upvote/${postId}`, {
                method: "POST",
                credentials: "include", // Ensure cookies are sent for auth
            });

            if (!response.ok) {
                throw new Error("Failed to upvote post.");
            }

            const data = await response.json();

            // Update the upvote count for the post in the state
            setPosts((prevPosts) =>
                prevPosts.map((post) =>
                    post.id === postId ? { ...post, upvotes: data.upvotes } : post
                )
            );
        } catch (err) {
            console.error(err);
            setError("Login to upvote a post");
        }
    };

    return (
        <Container className="mt-4">
            <div className="d-flex justify-content-between align-items-center mb-3">
                <h1 className="mb-0">Posts</h1>
                {user?.role === 'Admin' || user?.role === 'User' ? (
                    <Button href='/postcreate' className='btn btn-secondary mt-3'>Create New Post</Button>
                ) : null}
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
                    ? <PostList posts={posts} API_URL={API_URL} onDelete={confirmDeletePost} onUpvote={handleUpvote} />
                    : <PostGrid posts={posts} API_URL={API_URL} onDelete={confirmDeletePost} onUpvote={handleUpvote} />
            )}

            {/* Confirmation Modal */}
            <Modal show={showModal} onHide={cancelDelete}>
                <Modal.Header closeButton>
                    <Modal.Title>Confirm Deletion</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    Are you sure you want to delete this post? This action cannot be undone.
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={cancelDelete}>
                        Cancel
                    </Button>
                    <Button variant="danger" onClick={handleDeletePost}>
                        Delete
                    </Button>
                </Modal.Footer>
            </Modal>
        </Container>
    );
};

export default PostListPage;
