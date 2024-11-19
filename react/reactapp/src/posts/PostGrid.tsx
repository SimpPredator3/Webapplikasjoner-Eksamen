import React from 'react';
import { Card, Col, Row } from 'react-bootstrap';
import { Post } from '../types/Post'; // Import the Post type
import './PostGrid.css';


interface PostGridProps {
    posts: Post[];
    API_URL: string;
}

const PostGrid: React.FC<PostGridProps> = ({ posts, API_URL }) => (
    <Row xs={1} sm={2} md={3} className="g-4 post-grid">
        {posts.map((post) => (
            <Col key={post.id}>
                <Card className="post-card">
                    {post.imageUrl && (
                        <Card.Img
                            variant="top"
                            src={`${post.imageUrl}`}
                            alt={post.title}
                            style={{ height: '200px', objectFit: 'cover' }}
                        />
                    )}
                    <Card.Body>
                        <Card.Title className="post-card-title">{post.title}</Card.Title>
                        <Card.Subtitle className="mb-2 post-card-subtitle">By <span className="author">{post.author}</span></Card.Subtitle>
                        <Card.Text>{post.content.substring(0, 100)}...</Card.Text>
                        <Card.Text className="date"><small>{new Date(post.createdDate).toLocaleDateString()}</small></Card.Text>
                    </Card.Body>
                </Card>
            </Col>
        ))}
    </Row>
);

export default PostGrid;
