using System.ComponentModel.DataAnnotations;

namespace api.DTOs
{
    // Data Transfer Object for transferring comment data between the API and client
    public class CommentDto
    {
        // Unique identifier for the comment
        public int Id { get; set; }

        // The content of the comment (required and with a maximum length of 500 characters)
        [Required(ErrorMessage = "The comment is required")] // Ensures the comment field is not null or empty
        [StringLength(500, ErrorMessage = "The comment cannot exceed 500 characters")] // Restricts the length of the comment
        public string Comment { get; set; } = default!; // Initializes the property with a default value to avoid null issues

        // The date and time when the comment was created
        public DateTime CreatedDate { get; set; }

        // The author of the comment (required field)
        [Required] // Ensures the author field is not null or empty
        public string Author { get; set; } = default!;

        // The ID of the post to which this comment belongs
        public int PostId { get; set; }
    }
}
