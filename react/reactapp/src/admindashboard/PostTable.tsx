import React from 'react';
import { Table } from 'react-bootstrap';
import { Post } from '../types/Post';

interface PostTableProps {
    posts: Post[];
    API_URL: string;
}

const PostTable: React.FC<PostTableProps> = ({ posts, API_URL }) => (
    <Table striped bordered hover responsive="md" className="mt-4">
        <thead>
            <tr>
                <th>ID</th>
                <th>Title</th>
                <th>Author</th>
                <th>Content</th>
                <th>Image</th>
                <th>Created Date</th>
            </tr>
        </thead>
        <tbody>
            {posts.map((post) => (
                <tr key={post.id}>
                    <td>{post.id}</td>
                    <td>{post.title}</td>
                    <td>{post.author}</td>
                    <td>{post.content.substring(0, 100)}...</td>
                    <td>
                        {post.imageUrl ? (
                            <img
                                src={`${API_URL}${post.imageUrl}`}
                                alt={post.title}
                                style={{ width: '80px', height: 'auto' }}
                            />
                        ) : (
                            'No Image'
                        )}
                    </td>
                    <td>{new Date(post.createdDate).toLocaleDateString()}</td>
                </tr>
            ))}
        </tbody>
    </Table>
);

export default PostTable;
