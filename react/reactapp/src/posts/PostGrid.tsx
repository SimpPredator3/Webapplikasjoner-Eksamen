import React from 'react';
import { Card, Col, Row } from 'react-bootstrap';
import { Post } from '../types/Post'; // Import the Post type

interface PostGridProps {
    posts: Post[];
    API_URL: string;
}

const PostGrid: React.FC<PostGridProps> = ({ posts, API_URL }) => (
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
                        <Card.Subtitle className="mb-2 text-muted">By {post.author}</Card.Subtitle>
                        <Card.Text>{post.content.substring(0, 100)}...</Card.Text>
                        <Card.Text><small>{new Date(post.createdDate).toLocaleDateString()}</small></Card.Text>
                    </Card.Body>
                </Card>
            </Col>
        ))}
    </Row>
);

export default PostGrid;
