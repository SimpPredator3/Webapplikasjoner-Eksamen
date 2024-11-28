import React from 'react';
import { Card, Col, Row, Button } from 'react-bootstrap';
import { Post } from '../types/Post';
import './PostGrid.css';
import { useNavigate } from 'react-router-dom'; // Import useNavigate
import { useUser } from '../components/UserContext'; // Import useUser to get current user
import '../App.css';
import { PostComments } from "../components/PostComments";

interface MyPostProps {
    posts: Post[];
    API_URL: string;
    comments: any[];
    onDelete: (id: number) => void;
    onUpvote: (id: number) => Promise<void>;
    setVisibleCommentPostId: React.Dispatch<React.SetStateAction<number | null>>;
    visibleCommentPostId: number | null;
    onVote: (id: number, direction: "up" | "down") => void;
    onAddComment: (id: number, text: string) => void;
    onEditComment: (
        postId: number,
        commentId: number,
        text: string,
        author: string
    ) => void;
    onDeleteComment: (commentId: number) => void;
  
    fetchComments: (postId: number) => void;
}

const MyPost: React.FC<MyPostProps> = ({
    posts,
    API_URL,
    setVisibleCommentPostId,
    onUpvote,
    onAddComment,
    onEditComment,
    onDeleteComment,
    onDelete,
    fetchComments,
    comments,
    visibleCommentPostId,}) => {
    const navigate = useNavigate(); // Initialize navigate function
    const { user } = useUser(); // Get the current user from UserContext
    console.log("MyPost component mounted");

    // Filter posts to only show those authored by the current user
    const userPosts = posts.filter(post => post.author === user?.username);

    console.log('Current User:', user);

    if (!user) {
        return <p>Create a new user or log in to access your page.</p>;
    }
    if (userPosts.length === 0) {
        return <p>No posts found for the current user.</p>;
    }

    return (
        <Row xs={1} sm={2} md={3} className="g-4">
            {userPosts.map((post) => (
                <Col key={post.id}>
                    <Card>
                        {post.imageUrl && (
                            <Card.Img
                                variant="top"
                                src={`${post.imageUrl}`}
                                alt={post.title}
                                style={{ height: '200px', objectFit: 'cover' }}
                            />
                        )}
                        <Card.Body>
                            <Card.Title>{post.title}</Card.Title>
                            <Card.Subtitle className="mb-2 text-muted">
                                By <span className="author">{post.author}</span>
                            </Card.Subtitle>
                            <Card.Text>{post.tag}</Card.Text>
                            <Card.Text>{post.content.substring(0, 100)}...</Card.Text>
                            <Card.Text className="text-muted">
                                <small>{new Date(post.createdDate).toLocaleDateString()}</small>
                            </Card.Text>
                            <div className="d-flex justify-content-between align-items-center">
                                <Button
                                    variant="success"
                                    className="like-button"
                                    size="sm"
                                    onClick={() => onUpvote(post.id)}
                                >
                                    👍 {post.upvotes} Upvotes
                                </Button>
                            </div>
                            <div className="d-flex justify-content-between mt-2">

                                <Button
                                    variant="info"
                                    size="sm"
                                    onClick={() => {
                                    fetchComments(post.id);
                                    setVisibleCommentPostId((prev: number | null) =>
                                            prev === post.id ? null : post.id
                                        );
                                    }}
                                className="me-2 comment-button"
                                >
                                {visibleCommentPostId === post.id ? 'Hide Comments' : 'Show Comments'}
                                </Button>
                            </div>
                            {(user?.role === 'Admin' || user?.username === post.author) && (
                                <div className="d-flex justify-content-between mt-2">
                                    <Button
                                        variant="warning"
                                        size="sm"
                                        className="edit-button"
                                        onClick={() => navigate(`/post/edit/${post.id}`)} // Navigate to the edit page
                                    >
                                        Edit
                                    </Button>
                                    <Button
                                        variant="danger"
                                        size="sm"
                                        onClick={() => onDelete(post.id)} // Call the delete function
                                        className="me-2 delete-button"
                                    >
                                        Delete
                                    </Button>
                                </div>
                            )}
                        </Card.Body>
                    </Card>
                </Col>
            ))}
        </Row>
    );
};

export default MyPost;
