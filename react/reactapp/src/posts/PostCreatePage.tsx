import React from 'react';
import { useNavigate } from 'react-router-dom';
import PostForm from './PostForm';
import { Post } from '../types/Post';
import API_URL from '../apiConfig';

const PostCreatePage: React.FC = () => {
  const navigate = useNavigate(); // Create a navigate function

  const handlePostCreated = async (post: Post) => {
    try {
      const response = await fetch(`${API_URL}/api/Postapi/create`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(post),
      });

      if (!response.ok) {
        throw new Error('Network response was not ok');
      }

      const data = await response.json();
      console.log('Post created successfully:', data);
      navigate('/posts'); // Navigate back after successful creation
    } catch (error) {
      console.error('There was a problem with the fetch operation:', error);
    }
  }
  
  return (
    <div>
      <h2>Create New Post</h2>
      <PostForm onPostChanged={handlePostCreated}/>
    </div>
  );
};

export default PostCreatePage; 