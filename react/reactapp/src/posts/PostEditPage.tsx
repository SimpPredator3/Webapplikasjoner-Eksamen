import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { Form, Button, Container, Alert, Spinner } from 'react-bootstrap';
import { API_URL } from '../apiConfig';
import { Post } from '../types/Post';

const PostEditPage: React.FC = () => {
    const { id } = useParams<{ id: string }>();
    const navigate = useNavigate();

    const [post, setPost] = useState<Post | null>(null);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [saving, setSaving] = useState(false);

    useEffect(() => {
        const fetchPost = async () => {
            setLoading(true);
            try {
                const response = await fetch(`${API_URL}/api/post/${id}`);
                if (!response.ok) {
                    throw new Error('Failed to fetch post');
                }
                const data = await response.json();
                setPost(data);
            } catch (err) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };

        fetchPost();
    }, [id]);

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = e.target;
        setPost((prevPost) => (prevPost ? { ...prevPost, [name]: value } : null));
    };

    const handleSave = async () => {
        if (!post) return;

        setSaving(true);
        setError(null);

        try {
            const response = await fetch(`${API_URL}/api/post/${id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(post),
            });

            if (!response.ok) {
                throw new Error('Failed to update post');
            }

            navigate('/');
        } catch (err) {
            setError(err.message);
        } finally {
            setSaving(false);
        }
    };

    if (loading) {
        return (
            <Container className="text-center mt-5">
                <Spinner animation="border" role="status">
                    <span className="sr-only">Loading...</span>
                </Spinner>
            </Container>
        );
    }

    if (error) {
        return (
            <Container className="mt-5">
                <Alert variant="danger">{error}</Alert>
            </Container>
        );
    }

    return (
        <Container className="mt-5">
            <h1>Edit Post</h1>
            {post && (
                <Form>
                    <Form.Group controlId="formTitle" className="mt-3">
                        <Form.Label>Title</Form.Label>
                        <Form.Control
                            type="text"
                            name="title"
                            value={post.title}
                            onChange={handleInputChange}
                        />
                    </Form.Group>
                    <Form.Group controlId="formContent" className="mt-3">
                        <Form.Label>Content</Form.Label>
                        <Form.Control
                            as="textarea"
                            rows={5}
                            name="content"
                            value={post.content}
                            onChange={handleInputChange}
                        />
                    </Form.Group>
                    <Form.Group controlId="formImageUrl" className="mt-3">
                        <Form.Label>Image URL</Form.Label>
                        <Form.Control
                            type="text"
                            name="imageUrl"
                            value={post.imageUrl || ''}
                            onChange={handleInputChange}
                        />
                    </Form.Group>
                    {/* <Form.Group controlId="formTag" className="mt-3">
                        <Form.Label>Tag</Form.Label>
                        <Form.Control
                            type="text"
                            name="tag"
                            value={post.tag || ''}
                            onChange={handleInputChange}
                        />
                    </Form.Group> */}
                    <Button
                        className="mt-4"
                        variant="primary"
                        onClick={handleSave}
                        disabled={saving}
                    >
                        {saving ? 'Saving...' : 'Save Changes'}
                    </Button>
                    <Button
                        className="mt-4 ms-2"
                        variant="secondary"
                        onClick={() => navigate('/')}
                        disabled={saving}
                    >
                        Cancel
                    </Button>
                </Form>
            )}
        </Container>
    );
};

export default PostEditPage;