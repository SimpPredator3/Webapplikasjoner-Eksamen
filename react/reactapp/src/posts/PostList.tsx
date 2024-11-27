import React from 'react';
import { Button, Card } from 'react-bootstrap';
import { Post } from '../types/Post'; // Import the Post type
import { useNavigate } from 'react-router-dom'; // Import useNavigate
import './PostList.css';
import { useUser } from '../components/UserContext'; // Import useUser to get current user
import { PostComments } from "../components/PostComments";
import '../App.css';

interface PostListProps {
    posts: Post[];
    API_URL: string;
    comments: any[];
    setVisibleCommentPostId: React.Dispatch<React.SetStateAction<number | null>>;
    visibleCommentPostId: number | null;
    onDelete: (id: number) => void;
    onUpvote: (id: number) => Promise<void>;
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

const PostList: React.FC<PostListProps> = ({
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
    visibleCommentPostId,
}) => {
    const navigate = useNavigate(); // Initialize navigate function
    const { user } = useUser(); // Get the current user from UserContext

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
                                    style={{ width: "100%", height: "250px", objectFit: "cover" }}
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
                            <div className="d-flex justify-content-between align-items-center">
                                <Button
                                    variant="success"
                                    size="sm"
                                    onClick={() => onUpvote(post.id)}
                                >
                                    👍 {post.upvotes} Upvotes
                                </Button>
                                <span>{post.upvotes} Likes</span>
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
                                    className="me-2"
                                    style={{
                                        borderRadius: '20px', // Make the button rounded
                                        fontWeight: 'bold',
                                    }}
                                >
                                    {visibleCommentPostId === post.id ? 'Hide Comments' : 'Show Comments'}
                                </Button>
                            </div>
                        </Card.Body>
                    </div>
                    {visibleCommentPostId === post.id && (
                        <div className="px-6 pb-6">
                            <PostComments
                                postId={post.id}
                                comments={comments}
                                onAddComment={onAddComment}
                                onEditComment={onEditComment}
                                onDeleteComment={onDeleteComment}
                                author={post.author}
                            />
                        </div>
                    )}
                </Card>
            ))}
        </div>
    );
};

export default PostList;