import React, { useState, useEffect } from 'react';
import PostGrid from '../posts/PostGrid';
import PostList from '../posts/PostList';
import { Spinner, Alert, Button, Container, Modal } from 'react-bootstrap';
import { API_URL } from '../apiConfig';
import { Post } from '../types/Post';
import '../posts/PostListPage.css';
import { useNavigate } from 'react-router-dom';
import { useUser } from '../components/UserContext';
import '../App.css';

interface PostListPageProps {
    initialView?: "list" | "grid";
    lockedView?: "list" | "grid";
}

interface Comment {
    id: number;
    postId: number;
    text: string;
}

const AdminListPage: React.FC<PostListPageProps> = ({ initialView = "grid", lockedView }) => {
    const [posts, setPosts] = useState<Post[]>([]);
    const [loadingPosts, setLoadingPosts] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);
    const [view, setView] = useState<"list" | "grid">(lockedView ?? initialView);
    const [showModal, setShowModal] = useState<boolean>(false);
    const [searchTag, setSearchTag] = useState<string>("");
    const [postToDelete, setPostToDelete] = useState<number | null>(null);
    const [comments, setComments] = useState<Comment[]>([]);
    const [visibleCommentPostId, setVisibleCommentPostId] = useState<number | null>(null);
    const navigate = useNavigate();
    const { user, fetchUserRole, loading } = useUser();

    useEffect(() => {
        const checkAccess = async () => {
            if (loading) {
                console.log('Waiting for user context to finish loading...');
                return; // Wait until user details are fully loaded
            }
            if (!user) {
                console.log("User not logged in. Redirecting to login...");
                navigate('/login'); // Redirect to login if user is not authenticated
                return;
            }

            console.log("User's role:", user.role);
            if (user.role !== 'Admin') {
                console.log("User is not authorized. Redirecting to unauthorized...");
                navigate('/unauthorized'); // Redirect unauthorized users
                return;
            }

            console.log("User is Admin. Fetching posts...");
            fetchPosts();
        };

        checkAccess();
    }, [user, loading, navigate]);

    const fetchPosts = async () => {
        setLoadingPosts(true);
        setError(null);

        try {
            const response = await fetch(`${API_URL}/api/admindash`, { credentials: 'include' });
            if (!response.ok) {
                throw new Error('Failed to fetch posts');
            }
            const data: Post[] = await response.json();
            setPosts(data);
        } catch (err: any) {
            setError(err.message);
        } finally {
            setLoadingPosts(false);
        }
    };

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
            const response = await fetch(`${API_URL}/api/admindash/delete/${postToDelete}`, {
                method: 'DELETE',
            });

            if (!response.ok) {
                throw new Error('Failed to delete post');
            }

            setPosts(posts.filter(post => post.id !== postToDelete));
            setShowModal(false);
            setPostToDelete(null);
        } catch (err) {
            console.error(err.message);
            setError('Failed to delete the post.');
        }
    };

    const handleUpvote = async (postId: number) => {
        try {
            const response = await fetch(`${API_URL}/api/upvote/${postId}`, {
                method: "POST",
                credentials: "include",
            });

            if (!response.ok) {
                throw new Error("Failed to upvote post.");
            }

            const data = await response.json();

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

    const handleAddComment = async (postId: number, text: string) => {
        setLoadingPosts(true);
        setError(null);
        try {
            const response = await fetch(`${API_URL}/api/comment/${postId}`, {
                method: "POST",
                credentials: "include",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({ text }),
            });
            if (!response.ok) {
                throw new Error("Failed to add comment");
            }
            const data: Comment = await response.json();
            setComments((prev) => [...prev, data]);
        } catch (err) {
            setError(err.message);
        } finally {
            setLoadingPosts(false);
        }
    };

    const handleEditComment = async (postId: number, commentId: number, text: string, author: string) => {
        setLoadingPosts(true); // Corrected here
        setError(null);
        try {
            const response = await fetch(`${API_URL}/api/comment/${commentId}`, {
                method: "PUT",
                credentials: "include",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({
                    postId,
                    commentId,
                    text,
                    author,
                    createdDate: new Date(), // Assuming the creation date is required for the request
                }),
            });
            if (!response.ok) {
                throw new Error("Failed to edit comment");
            }
            setComments((prev) =>
                prev.map((comment) =>
                    comment.id === commentId ? { ...comment, text } : comment
                )
            );
        } catch (err) {
            setError(err.message);
        } finally {
            setLoadingPosts(false); // Corrected here
        }
    };


    const handleDeleteComment = async (commentId: number) => {
        setLoadingPosts(true);
        setError(null);
        try {
            const response = await fetch(`${API_URL}/api/comment/${commentId}`, {
                method: "DELETE",
                credentials: "include",
            });
            if (!response.ok) {
                throw new Error("Failed to delete comment");
            }
            setComments((prev) => prev.filter((comment) => comment.id !== commentId));
        } catch (err) {
            setError(err.message);
        } finally {
            setLoadingPosts(false);
        }
    };

    const fetchComments = async (postId: number) => {
        setLoadingPosts(true);
        setError(null);
        try {
            const response = await fetch(`${API_URL}/api/comment/${postId}`);
            if (!response.ok) {
                throw new Error("Failed to fetch comments");
            }
            const data: Comment[] = await response.json();
            setComments(data);
        } catch (err) {
            setError(err.message);
        } finally {
            setLoadingPosts(false);
        }
    };

    if (loading) {
        return (
            <div className="text-center">
                <Spinner animation="border" role="status">
                    <span className="sr-only">Loading...</span>
                </Spinner>
            </div>
        );
    }

    return (
        <Container className="admin-dashboard-container mt-4">
            <h1 className="mb-0 admin-titel">Admin Dashboard</h1>
            <div className="d-flex justify-content-between align-items-center mb-3">
                <input
                    type="text"
                    placeholder="Search by tag"
                    value={searchTag}
                    onChange={(e) => setSearchTag(e.target.value)}
                    className="search-bar form-control"
                />
                {user?.role === 'Admin' && (
                    <Button href='/postcreate' className='admin-post-btn create-btn btn btn-secondary'>Create New Post</Button>
                )}
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

            {loadingPosts && (
                <div className="text-center">
                    <Spinner animation="border" role="status">
                        <span className="sr-only">Loading...</span>
                    </Spinner>
                </div>
            )}
            {error && <Alert variant="danger">{error}</Alert>}
            {error && <Alert variant="danger">{error}</Alert>}
            {!loading &&
                !error &&
                ((lockedView ?? view) === "list" ? (
                    <PostList
                        posts={posts}
                        API_URL={API_URL}
                        onDelete={confirmDeletePost}
                        onUpvote={handleUpvote}
                        onAddComment={handleAddComment}
                        onEditComment={handleEditComment}
                        onDeleteComment={handleDeleteComment}
                        onVote={handleUpvote}
                        comments={comments}
                        fetchComments={fetchComments}
                        setVisibleCommentPostId={setVisibleCommentPostId}
                        visibleCommentPostId={visibleCommentPostId}
                    />
                ) : (
                    <PostGrid
                        posts={posts}
                        API_URL={API_URL}
                        onDelete={confirmDeletePost}
                        onUpvote={handleUpvote}
                        onAddComment={handleAddComment}
                        onEditComment={handleEditComment}
                        onDeleteComment={handleDeleteComment}
                        onVote={handleUpvote}
                        comments={comments}
                        fetchComments={fetchComments}
                        setVisibleCommentPostId={setVisibleCommentPostId}
                        visibleCommentPostId={visibleCommentPostId}
                    />
                ))}

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

export default AdminListPage;