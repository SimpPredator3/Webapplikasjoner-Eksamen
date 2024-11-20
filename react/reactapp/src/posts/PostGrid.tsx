import React from 'react';
import { Card, Col, Row, Button } from 'react-bootstrap';
import { Post } from '../types/Post';
import './PostGrid.css';
import { useNavigate } from 'react-router-dom'; // Import useNavigate


interface PostGridProps {
    posts: Post[];
    API_URL: string;
    onDelete: (id: number) => void;
    onUpvote: (id: number) => Promise<void>;
}

const PostGrid: React.FC<PostGridProps> = ({ posts, API_URL, onDelete, onUpvote }) => {
    const navigate = useNavigate(); // Initialize navigate function

    return (
        <Row xs={1} sm={2} md={3} className="g-4">
            {posts.map((post) => (
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
                            <Card.Text>{post.content.substring(0, 100)}...</Card.Text>
                            <Card.Text className="text-muted">
                                <small>{new Date(post.createdDate).toLocaleDateString()}</small>
                            </Card.Text>
                            <div className="d-flex justify-content-between align-items-center">
                                <Button
                                    variant="success"
                                    size="sm"
                                    onClick={() => onUpvote(post.id)}
                                >
                                    👍 {post.upvotes} Upvotes
                                </Button>
                            </div>
                            <div className="d-flex justify-content-between">
                                <Button
                                    variant="warning"
                                    size="sm"
                                    onClick={() => navigate(`/post/edit/${post.id}`)} // Navigate to the edit page
                                >
                                    Edit
                                </Button>
                                <Button
                                    variant="danger"
                                    size="sm"
                                    onClick={() => onDelete(post.id)} // Call the delete function
                                    className="me-2"
                                >
                                    Delete
                                </Button>
                            </div>
                        </Card.Body>
                    </Card>
                </Col>
            ))}
        </Row>
    );
};

export default PostGrid;
