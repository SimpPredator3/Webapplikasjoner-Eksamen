import React, { useState, useEffect } from 'react';
import PostGrid from './PostGrid';
import PostList from './PostList';
import MyPost from './MyPost';
import { Spinner, Alert, Button, Container, Modal } from 'react-bootstrap';
import { API_URL } from '../apiConfig';
import { Post } from '../types/Post';
import './PostListPage.css';
import { useNavigate, useLocation } from 'react-router-dom';
import { useUser } from '../components/UserContext';
import '../App.css';

interface PostListPageProps {
    initialView?: "list" | "grid"; // Optional prop for initial view
    lockedView?: "list" | "grid";  // Optional prop to lock the view
}

interface Comment {
    id: number;
    postId: number;
    text: string;
}

const PostListPage: React.FC<PostListPageProps> = ({ initialView = "grid", lockedView }) => {
    const [posts, setPosts] = useState<Post[]>([]);
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);
    const [view, setView] = useState<"list" | "grid" | "MyPost">(lockedView ?? initialView);
    const [showModal, setShowModal] = useState<boolean>(false);
    const [postToDelete, setPostToDelete] = useState<number | null>(null);
    const [searchTag, setSearchTag] = useState<string>("");
    const [comments, setComments] = useState<Comment[]>([]);
    const [visibleCommentPostId, setVisibleCommentPostId] = useState<number | null>(null);

    const navigate = useNavigate();
    const location = useLocation();
    const { user } = useUser();

    // Fetch posts from API
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

    // Filter posts by tag
    const filteredPosts = posts.filter(post =>
        (post.tag?.toLowerCase() || "").includes(searchTag.toLowerCase())
    );

    // Update view based on navigation state
    useEffect(() => {
        const stateView = location.state?.view as "list" | "grid" | "MyPost";
        if (stateView && !lockedView) { // Avoid overriding if `view` is manually changed
            setView(stateView);
        }
    }, [location.state, lockedView]);

    useEffect(() => {
        fetchPosts();
    }, [user]);

    const postHandlers = {
        onUpvote: async (postId: number) => {
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
        },
        onDelete: (id: number) => {
            setPostToDelete(id);
            setShowModal(true);
        },
    };

    const commentHandlers = {
        fetchComments: async (postId: number) => {
            setLoading(true);
            setError(null);
            try {
                const response = await fetch(`${API_URL}/api/comment/${postId}`);
                if (!response.ok) {
                    throw new Error("Failed to fetch comments");
                }
                const data: Comment[] = await response.json();
                setComments(data);
            } catch (err: any) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        },
        onAddComment: async (postId: number, text: string) => {
            if (!user) {
                setError("You must be logged in to add a comment.");
                return;
            }
            setLoading(true);
            try {
                const response = await fetch(`${API_URL}/api/comment/${postId}`, {
                    method: "POST",
                    credentials: "include",
                    headers: { "Content-Type": "application/json" },
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
                setLoading(false);
            }
        },
        onEditComment: async (
            postId: number,
            commentId: number,
            text: string,
            author: string
        ) => {
            setLoading(true);
            try {
                const response = await fetch(`${API_URL}/api/comment/${commentId}`, {
                    method: "PUT",
                    credentials: "include",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({ comment: text, postId, author }),
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
                setLoading(false);
            }
        },
        onDeleteComment: async (commentId: number) => {
            setLoading(true);
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
                setLoading(false);
            }
        },
        visibleCommentPostId,
        setVisibleCommentPostId,
    };

    return (
        <Container className="admin-dashboard-container mt-4">
            {/* Tag Search Input */}
            <div className="d-flex justify-content-between align-items-center mb-3">
                <input
                    type="text"
                    placeholder="Search by tag"
                    value={searchTag}
                    onChange={(e) => setSearchTag(e.target.value)}
                    className="form-control"
                    style={{ width: '200px' }}
                />
                <Button href='/postcreate' className='admin-post-btn create-btn btn btn-secondary'>Create New Post</Button>
                <div className="d-flex">
                    <button
                        onClick={() => setView("grid")}
                        className={`btn me-2 ${view === "grid" ? "active-btn" : "inactive-btn"}`}
                        title="Grid View"
                    >
                        <i className="fas fa-th"></i>
                    </button>
                    <button
                        onClick={() => setView("list")}
                        className={`btn me-2 ${view === "list" ? "active-btn" : "inactive-btn"}`}
                        title="List View"
                    >
                        <i className="fas fa-list"></i>
                    </button>
                    <button
                        onClick={() => navigate('/posts', { state: { view: "MyPost" } })}
                        className={`btn ${view === "MyPost" ? "active-btn" : "inactive-btn"}`}
                        title="My Posts"
                    >
                        <i className="fas fa-user"></i>
                    </button>
                </div>
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
                (lockedView ?? view) === "list" ? (
                    <PostList
                        posts={filteredPosts}
                        API={{ API_URL }}
                        commentHandlers={commentHandlers}
                        postHandlers={postHandlers}
                        comments={comments}
                    />
                ) : view === "MyPost" ? (
                    <MyPost
                        posts={filteredPosts}
                        API={{ API_URL }}
                        commentHandlers={commentHandlers}
                        postHandlers={postHandlers}
                        comments={comments}
                    />
                ) : (
                    <PostGrid
                        posts={filteredPosts}
                        API={{ API_URL }}
                        commentHandlers={commentHandlers}
                        postHandlers={postHandlers}
                        comments={comments}
                    />
                )
            )}

            {/* Confirmation Modal */}
            <Modal show={showModal} onHide={() => setShowModal(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>Confirm Deletion</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    Are you sure you want to delete this post? This action cannot be undone.
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={() => setShowModal(false)}>
                        Cancel
                    </Button>
                    <Button variant="danger" onClick={() => {
                        postHandlers.onDelete(postToDelete as number);
                        setShowModal(false);
                    }}>
                        Delete
                    </Button>
                </Modal.Footer>
            </Modal>
        </Container>
    );
};

export default PostListPage;
