import React from 'react';
import { Card } from 'react-bootstrap';
import { Post } from '../types/Post'; // Import the Post type
import './PostList.css';

interface PostGridProps {
    posts: Post[];
    API_URL: string;
}

const PostList: React.FC<PostGridProps> = ({ posts, API_URL }) => (
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
                    </Card.Body>
                </div>
            </Card>
        ))}
    </div>
);

export default PostList;
