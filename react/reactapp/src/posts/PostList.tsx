import React from 'react';
import { Button, Card } from 'react-bootstrap';
import { Post } from '../types/Post'; // Import the Post type
import { useNavigate } from 'react-router-dom'; // Import useNavigate
import './PostList.css';

interface PostListProps {
    posts: Post[];
    API_URL: string;
    onDelete: (id: number) => void;
}

const PostList: React.FC<PostListProps> = ({ posts, API_URL, onDelete }) => {
    const navigate = useNavigate(); // Initialize navigate function

    return (
        <div className="post-list">
            {posts.map((post) => (
                <Card key={post.id} className="post-list-card mb-4">
                    <div className="d-flex flex-column flex-md-row">
                        {post.imageUrl && (
                            <div className="post-list-image">
                                <Card.Img
                                    src={`${post.imageUrl}`}
                                    alt={post.title}
                                    style={{ width: '100%', height: '250px', objectFit: 'cover' }}
                                />
                            </div>
                        )}
                        <Card.Body className="post-list-body">
                            <Card.Title className="post-list-title">{post.title}</Card.Title>
                            <Card.Subtitle className="mb-2 text-muted">
                                By <span className="author">{post.author}</span>
                            </Card.Subtitle>
                            <Card.Text>{post.content}</Card.Text>
                            <Card.Text className="text-muted">
                                <small>{new Date(post.createdDate).toLocaleDateString()}</small>
                            </Card.Text>
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
                        </Card.Body>
                    </div>
                </Card>
            ))}
        </div>
    );
};

export default PostList;