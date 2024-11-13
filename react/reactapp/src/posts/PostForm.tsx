import React, { useState } from 'react';
import { Form, Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { Post } from '../types/Post'; // Ensure this path is correct

interface PostFormProps {
  onPostCreated: (newPost: Post) => void; // Define `onPostCreated` in props
}

const PostForm: React.FC<PostFormProps> = ({ onPostCreated }) => {
  const [title, setTitle] = useState('');
  const [content, setContent] = useState('');
  const [imageUrl, setImageUrl] = useState('');
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();

    const newPost: Post = {
      id: 0, // Temporarily set to 0, assuming the backend will assign a real ID
      title,
      author: 'Default Author', // Set a default author or collect it from a form field
      content,
      imageUrl: imageUrl || undefined,
      createdDate: new Date().toISOString()
    };

    try {
      onPostCreated(newPost); // Call the function passed from PostCreatePage
    } catch (err: any) {
      setError(err.message || 'An unexpected error occurred');
    }
  };

  return (
    <Form onSubmit={handleSubmit}>
      <Form.Group controlId="formPostTitle">
        <Form.Label>Title</Form.Label>
        <Form.Control
          type="text"
          placeholder="Enter Post title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          required
        />
      </Form.Group>

      <Form.Group controlId="formPostContent">
        <Form.Label>Content</Form.Label>
        <Form.Control
          as="textarea"
          rows={3}
          placeholder="Enter post content"
          value={content}
          onChange={(e) => setContent(e.target.value)}
        />
      </Form.Group>

      <Form.Group controlId="formPostImageUrl">
        <Form.Label>Image URL</Form.Label>
        <Form.Control
          type="text"
          placeholder="Enter image URL"
          value={imageUrl}
          onChange={(e) => setImageUrl(e.target.value)}
        />
      </Form.Group>

      {error && <p style={{ color: 'red' }}>{error}</p>}

      <Button variant="primary" type="submit">Create Post</Button>
      <Button variant="secondary" onClick={() => navigate(-1)} className="ms-2">Cancel</Button>
    </Form>
  );
};

export default PostForm;
