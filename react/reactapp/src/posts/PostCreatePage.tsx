import React from 'react';
import { useNavigate } from 'react-router-dom';
import PostForm from './PostForm';
import { Post } from '../types/Post';
import { API_URL } from '../apiConfig';
import '../App.css';

const PostCreatePage: React.FC = () => {
  const navigate = useNavigate();

  const handlePostCreated = async (post: Post) => {
    try {
      const response = await fetch(`${API_URL}/api/post/create`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(post),
      });

      if (!response.ok) {
        throw new Error('Failed to create post');
      }

      const data = await response.json();
      console.log('Post created successfully:', data);

      // Redirect to posts page after successful creation
      navigate('/');
    } catch (error) {
      console.error('There was a problem with the fetch operation:', error);
    }
  };

  return (
    <div>
      <h2>Create New Post</h2>
      <PostForm onPostCreated={handlePostCreated} /> {/* Pass handlePostCreated as onPostCreated */}
    </div>
  );
};

export default PostCreatePage;
