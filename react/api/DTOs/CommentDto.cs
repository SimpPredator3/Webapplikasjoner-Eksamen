using System.ComponentModel.DataAnnotations;

namespace api.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The comment is required")]
        [StringLength(500, ErrorMessage = "The comment cannot exceed 500 characters")]
        public string Comment { get; set; } = default!;

        public DateTime CreatedDate { get; set; }

        [Required]
        public string Author { get; set; } = default!;

        public int PostId { get; set; }
    }
}