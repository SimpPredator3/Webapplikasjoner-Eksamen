import React from 'react';
import { Card, Col, Row, Button } from 'react-bootstrap';
import { Post } from '../types/Post';
import './PostGrid.css';
import { useNavigate } from 'react-router-dom';
import { useUser } from '../components/UserContext';
import '../App.css';
import { PostComments } from "../components/PostComments";

interface CommentHandlers {
    fetchComments: (postId: number) => void;
    setVisibleCommentPostId: React.Dispatch<React.SetStateAction<number | null>>;
    visibleCommentPostId: number | null;
    onAddComment: (postId: number, text: string) => void;
    onEditComment: (
        postId: number,
        commentId: number,
        text: string,
        author: string
    ) => void;
    onDeleteComment: (commentId: number) => void;
}

interface PostHandlers {
    onUpvote: (id: number) => Promise<void>;
    onDelete: (id: number) => void;
}

interface MyPostProps {
    posts: Post[];
    API: { API_URL: string };
    commentHandlers: CommentHandlers;
    postHandlers: PostHandlers;
    comments: any[];
}

const MyPost: React.FC<MyPostProps> = ({
    posts,
    commentHandlers,
    postHandlers,
    comments,
}) => {
    const navigate = useNavigate();
    const { user } = useUser();

    // Filter posts to only show those authored by the current user
    const userPosts = posts.filter(post => post.author === user?.username);

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
                                    size="sm"
                                    className="like-button"
                                    onClick={() => postHandlers.onUpvote(post.id)}
                                >
                                    👍 {post.upvotes} Upvotes
                                </Button>
                            </div>
                            <div className="d-flex justify-content-between mt-2">
                                <Button
                                    variant="info"
                                    size="sm"
                                    onClick={() => {
                                        commentHandlers.fetchComments(post.id);
                                        commentHandlers.setVisibleCommentPostId((prev: number | null) =>
                                            prev === post.id ? null : post.id
                                        );
                                    }}
                                    className="me-2 comment-button"
                                >
                                    {commentHandlers.visibleCommentPostId === post.id
                                        ? 'Hide Comments'
                                        : 'Show Comments'}
                                </Button>
                            </div>
                            {(user?.role === 'Admin' || user?.username === post.author) && (
                                <div className="d-flex justify-content-between mt-2">
                                    <Button
                                        variant="warning"
                                        size="sm"
                                        className="edit-button"
                                        onClick={() => navigate(`/post/edit/${post.id}`)}
                                    >
                                        Edit
                                    </Button>
                                    <Button
                                        variant="danger"
                                        size="sm"
                                        onClick={() => postHandlers.onDelete(post.id)}
                                        className="me-2 delete-button"
                                    >
                                        Delete
                                    </Button>
                                </div>
                            )}
                        </Card.Body>
                        {commentHandlers.visibleCommentPostId === post.id && (
                            <div className="px-6 pb-6">
                                <PostComments
                                    postId={post.id}
                                    comments={comments}
                                    onAddComment={commentHandlers.onAddComment}
                                    onEditComment={commentHandlers.onEditComment}
                                    onDeleteComment={commentHandlers.onDeleteComment}
                                    author={post.author}
                                />
                            </div>
                        )}
                    </Card>
                </Col>
            ))}
        </Row>
    );
};

export default MyPost;
