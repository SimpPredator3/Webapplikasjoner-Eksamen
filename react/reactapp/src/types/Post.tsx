export interface Post { 
    id: number;
    title: string;
    author: string;
    content: string;
    imageUrl?: string; // Optional, as indicated by the question mark
    createdDate: string; 
}