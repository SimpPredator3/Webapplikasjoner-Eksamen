import React, { useState, useEffect } from 'react';
import PostTable from './PostTable';
import PostGrid from './PostGrid';
import { Spinner, Alert, Button, Container } from 'react-bootstrap';
import { API_URL } from '../apiConfig';
import { Post } from '../types/Post';

interface PostListPageProps {
    initialView?: "table" | "grid"; // Optional prop for initial view
    lockedView?: "table" | "grid";  // Optional prop to lock the view
}

const PostListPage: React.FC<PostListPageProps> = ({ initialView = "grid", lockedView }) => {
    const [posts, setPosts] = useState<Post[]>([]);
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);
    const [view, setView] = useState<"table" | "grid">(lockedView ?? initialView);

    const fetchPosts = async () => {
        setLoading(true);
        setError(null);

        try {
            const response = await fetch(`${API_URL}/api/post`);
            if (!response.ok) {
                throw new Error('Failed to fetch posts');
            }
            const data: Post[] = await response.json();
            setPosts(data.slice(0, 20));
        } catch (err: any) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchPosts();
    }, []);

    const toggleView = () => {
        if (!lockedView) {
            setView(prevView => (prevView === "grid" ? "table" : "grid"));
        }
    };

    return (
        <Container className="mt-4">
            <div className="d-flex justify-content-between align-items-center mb-3">
                <h1 className="mb-0">Posts</h1>
                {!lockedView && (
                    <Button onClick={toggleView} variant="primary">
                        {view === "table" ? "Switch to Grid View" : "Switch to Table View"}
                    </Button>
                )}
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
                (lockedView ?? view) === "table" 
                    ? <PostTable posts={posts} API_URL={API_URL} /> 
                    : <PostGrid posts={posts} API_URL={API_URL} />
            )}
            <Button href='/postcreate' className='btn btn-secondary mt-3'>Create New Post</Button>
        </Container>
    );
};

export default PostListPage;
