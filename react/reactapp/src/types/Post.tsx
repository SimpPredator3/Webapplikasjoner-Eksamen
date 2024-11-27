export interface Post {
    id: number;
    title: string;
    author: string;
    content: string;
    imageUrl?: string;
    createdDate: string;
    tag?:string;
    upvotes: number;
}