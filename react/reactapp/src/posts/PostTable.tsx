import React, { useState } from 'react';
import { Table, Button } from 'react-bootstrap';
import { Post } from '../types/Post'; // Import Post type
import '../App.css';

interface PostTableProps {
    posts: Post[];
    API_URL: string;
}

const PostTable: React.FC<PostTableProps> = ({ posts, API_URL }) => {
    const [showImage, setShowImage] = useState<boolean>(true);
    const [showContent, setShowContent] = useState<boolean>(true);

    const toggleImage = () => setShowImage(prevShowImage => !prevShowImage);
    const toggleContent = () => setShowContent(prevShowContent => !prevShowContent);

    return (
        <div>
            <Button onClick={toggleContent} className="btn btn-secondary mb-3 me-2">
                {showContent ? 'Hide Content' : 'Show Content'}
            </Button> 
            <Button onClick={toggleImage} className="btn btn-secondary mb-3">
                {showImage ? 'Hide Image' : 'Show Image'}
            </Button>
            <Table striped bordered hover responsive="md" className="mt-4">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Title</th>
                        <th>Author</th>
                        {showContent && <th>Content</th>}
                        {showImage && <th>Image</th>}
                        <th>Created Date</th>
                    </tr>
                </thead>
                <tbody>
                    {posts.map((post) => (
                        <tr key={post.id}>
                            <td>{post.id}</td>
                            <td>{post.title}</td>
                            <td>{post.author}</td>
                            {showContent && <td>{post.content.substring(0, 100)}...</td>}
                            {showImage && <td><img src={`${API_URL}${post.imageUrl}`} width="120" alt={post.title} /></td>}
                            <td>{new Date(post.createdDate).toLocaleDateString()}</td>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </div>
    );
};

export default PostTable;
