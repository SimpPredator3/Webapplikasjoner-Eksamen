import React, { useState, useEffect, useContext } from 'react';
import { Form, Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { Post } from '../types/Post'; // Ensure this path is correct
import { useUser } from '../components/UserContext'; // Import useUser to get the logged-in user

interface PostFormProps {
  onPostCreated: (newPost: Post) => void; // Define `onPostCreated` in props
}

const PostForm: React.FC<PostFormProps> = ({ onPostCreated }) => {
  const [title, setTitle] = useState('');
  const [content, setContent] = useState('');
  const [imageUrl, setImageUrl] = useState('');
  const [error, setError] = useState<string | null>(null);
  const { user } = useUser(); // Get the logged-in user from useUser
  const navigate = useNavigate();

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();

    // Client-side validation
    if (title.length < 5) {
      setError('Title must be at least 5 characters long');
      return;
    }

    if (content.length < 10) {
      setError('Content must be at least 10 characters long');
      return;
    }

    const newPost: Post = {
      id: 0, // Temporarily set to 0, assuming the backend will assign a real ID
      title,
      author: user?.username || user?.email || 'Unknown Author', // Set author to the logged-in user's username or email
      content,
      imageUrl: imageUrl || undefined,
      createdDate: new Date().toISOString(),
      upvotes: 0
    };

    try {
      onPostCreated(newPost); // Call the function passed from PostCreatePage
      setError(null); // Clear error if post is successfully created
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
          required
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
