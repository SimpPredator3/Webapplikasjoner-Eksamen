import { useState } from "react";
import { Button, Modal, Form } from "react-bootstrap";

interface Comment {
  id: number;
  text: string;
}

interface PostCommentsProps {
  postId: number;
  author: string;
  comments: Comment[];
  onAddComment: (postId: number, text: string) => void;
  onEditComment: (postId: number, commentId: number, text: string,author:string) => void;
  onDeleteComment: (commentId: number) => void;
}

export function PostComments({
  postId,
  comments,
  onAddComment,
  onEditComment,
  onDeleteComment,
  author,
}: PostCommentsProps) {
  const [newComment, setNewComment] = useState("");
  const [editingCommentId, setEditingCommentId] = useState<number | null>(null);
  const [editedCommentText, setEditedCommentText] = useState("");
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [deleteTargetCommentId, setDeleteTargetCommentId] = useState<
    number | null
  >(null);
  console.log(comments);

  const handleAddComment = (e: React.FormEvent) => {
    e.preventDefault();
    if (newComment.trim()) {
      onAddComment(postId, newComment);
      setNewComment("");
    }
  };

  const handleEditComment = (commentId: number) => {
    onEditComment(postId, commentId, editedCommentText,author);
    setEditingCommentId(null);
    setEditedCommentText("");
  };

  const handleDeleteComment = () => {
    if (deleteTargetCommentId !== null) {
      onDeleteComment(deleteTargetCommentId);
      setShowDeleteModal(false);
      setDeleteTargetCommentId(null);
    }
  };

  return (
    <div className="mb-4">
      <Form onSubmit={handleAddComment} className="d-flex gap-2">
        <Form.Control
          as="textarea"
          placeholder="Add a comment..."
          value={newComment}
          onChange={(e) => setNewComment(e.target.value)}
          className="min-height-60"
        />
        <Button variant="primary" type="submit">
          Comment
        </Button>
      </Form>
      <div className="mt-3">
        {comments.map((comment) => (
          <div key={comment.id} className="p-3  rounded mb-2 shadow-sm">
            {editingCommentId === comment.id ? (
              <div>
                <Form.Control
                  as="textarea"
                  value={editedCommentText}
                  onChange={(e) => setEditedCommentText(e.target.value)}
                  className="mb-2"
                />
                <div className="d-flex gap-2">
                  <Button
                    size="sm"
                    variant="success"
                    onClick={() => handleEditComment(comment.id, )}
                  >
                    Save
                  </Button>
                  <Button
                    size="sm"
                    variant="secondary"
                    onClick={() => {
                      setEditingCommentId(null);
                      setEditedCommentText("");
                    }}
                  >
                    Cancel
                  </Button>
                </div>
              </div>
            ) : (
              <div className="d-flex justify-content-between align-items-start">
                <p className="mb-0">{comment.text}</p>
                <div className="d-flex gap-2">
                  <Button
                    size="sm"
                    variant="outline-primary"
                    onClick={() => {
                      setEditingCommentId(comment.id);
                      setEditedCommentText(comment.text);
                    }}
                  >
                    Edit
                  </Button>
                  <Button
                    size="sm"
                    variant="outline-danger"
                    onClick={() => {
                      setDeleteTargetCommentId(comment.id);
                      setShowDeleteModal(true);
                    }}
                  >
                    Delete
                  </Button>
                </div>
              </div>
            )}
          </div>
        ))}
      </div>

      {/* Delete Confirmation Modal */}
      <Modal show={showDeleteModal} onHide={() => setShowDeleteModal(false)}>
        <Modal.Header closeButton>
          <Modal.Title>Delete Comment</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          Are you sure you want to delete this comment? This action cannot be
          undone.
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={() => setShowDeleteModal(false)}>
            Cancel
          </Button>
          <Button variant="danger" onClick={handleDeleteComment}>
            Delete
          </Button>
        </Modal.Footer>
      </Modal>
    </div>
  );
}
