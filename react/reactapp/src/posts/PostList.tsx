import React, { useState } from 'react';
import { Button, Card } from 'react-bootstrap';
import { Post } from '../types/Post'; // Import the Post type
import { useNavigate } from 'react-router-dom'; // Import useNavigate
import './PostList.css';
import { useUser } from '../components/UserContext'; // Import useUser to get current user
import { PostComments } from "../components/PostComments";
import '../App.css';

interface CommentHandlers {
    fetchComments: (postId: number) => void;
    onAddComment: (id: number, text: string) => void;
    onEditComment: (postId: number, commentId: number, text: string, author: string) => void;
    onDeleteComment: (commentId: number) => void;
    visibleCommentPostId: number | null;
    setVisibleCommentPostId: React.Dispatch<React.SetStateAction<number | null>>;
}

interface PostHandlers {
    onUpvote: (id: number) => Promise<void>;
    onDelete: (id: number) => void;
}

interface PostListProps {
    posts: Post[];
    API: { API_URL: string };
    commentHandlers: CommentHandlers;
    postHandlers: PostHandlers;
    comments: any[]; // Keep the comments as a separate prop
    showEditDelete: boolean;
}

const MAX_CONTENT_LENGTH = 100; // Length threshold for showing "Show More" button

const PostList: React.FC<PostListProps> = ({
    posts,
    API,
    commentHandlers,
    postHandlers,
    comments,
    showEditDelete,
}) => {
    const navigate = useNavigate(); // Initialize navigate function
    const { user } = useUser(); // Get the current user from UserContext
    const [expandedPostId, setExpandedPostId] = useState<number | null>(null);

    const handleExpandPost = (postId: number) => {
        setExpandedPostId((prevId) => (prevId === postId ? null : postId));
    }

    return (
        <div className="post-list">
            {posts.map((post) => {
                // Determine the image URL
                const imageUrl = post.imageUrl && post.imageUrl.trim() !== ""
                    ? `/${post.imageUrl}`
                    : "/images/default-placeholder.webp";

                const isExpanded = expandedPostId === post.id;

                // Determine if "Show More/Less" button should be displayed
                const shouldShowToggleButton = post.content.length > MAX_CONTENT_LENGTH;

                return (
                    <Card key={post.id} className="post-list-card mb-4" onClick={() => handleExpandPost(post.id)}>
                        <div className="d-flex flex-column flex-md-row">
                            <Card.Img
                                src={imageUrl}
                                alt={post.title}
                                className="post-list-image"
                            />
                            <Card.Body className="post-list-body">
                                <Card.Title className="post-list-title">{post.title}</Card.Title>
                                <Card.Subtitle className="mb-2 text-muted">
                                    By <span className="author">{post.author}</span>
                                </Card.Subtitle>

                                {/* Conditionally render minimized or full content */}
                                <Card.Text>
                                    {isExpanded ? post.content : post.content.substring(0, MAX_CONTENT_LENGTH) + (post.content.length > MAX_CONTENT_LENGTH ? '...' : '')}
                                </Card.Text>

                                {/* Expand/Collapse button */}
                                {shouldShowToggleButton && (
                                    <Button
                                        variant=""
                                        size="sm"
                                        onClick={(e) => {
                                            e.stopPropagation();
                                            handleExpandPost(post.id);
                                        }}
                                        className="expand-button"
                                    >
                                        {isExpanded ? 'Show less...' : 'Show more...'}
                                    </Button>
                                )}

                                <Card.Text className="text-muted">
                                    <small>{new Date(post.createdDate).toLocaleDateString()}</small>
                                </Card.Text>

                                {post.tag && <Card.Text>#{post.tag}</Card.Text>}

                                <div className="d-flex justify-content-between align-items-center">
                                    <Button
                                        variant="success"
                                        size="sm"
                                        className="like-button"
                                        onClick={(e) => {
                                            e.stopPropagation(); // Prevent expanding on button click
                                            postHandlers.onUpvote(post.id);
                                        }}
                                    >
                                        👍 {post.upvotes} Upvotes
                                    </Button>
                                    <span>{post.upvotes} Likes</span>
                                </div>

                                <div className="d-flex justify-content-between mt-2">
                                    <Button
                                        variant="info"
                                        size="sm"
                                        onClick={(e) => {
                                            e.stopPropagation(); // Prevent expanding on button click
                                            commentHandlers.fetchComments(post.id);
                                            commentHandlers.setVisibleCommentPostId((prev: number | null) =>
                                                prev === post.id ? null : post.id
                                            );
                                        }}
                                        className="me-2 comment-button"
                                    >
                                        {commentHandlers.visibleCommentPostId === post.id ? 'Hide Comments' : 'Show Comments'}
                                    </Button>
                                </div>

                                {showEditDelete && (user?.role === 'Admin' || user?.username === post.author) && (
                                    <div className="d-flex justify-content-between mt-2">
                                        <Button
                                            variant="warning"
                                            size="sm"
                                            className="edit-button"
                                            onClick={(e) => {
                                                e.stopPropagation(); // Prevent expanding on button click
                                                navigate(`/post/edit/${post.id}`);
                                            }}
                                        >
                                            Edit
                                        </Button>
                                        <Button
                                            variant="danger"
                                            size="sm"
                                            onClick={(e) => {
                                                e.stopPropagation(); // Prevent expanding on button click
                                                postHandlers.onDelete(post.id);
                                            }}
                                            className="me-2 delete-button"
                                        >
                                            Delete
                                        </Button>
                                    </div>
                                )}
                            </Card.Body>
                        </div>

                        {/* Show comments if they are visible */}
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
                );
            })}
        </div>
    );
};

export default PostList;