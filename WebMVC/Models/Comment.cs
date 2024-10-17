using System;
using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Comment text is required")]
        [StringLength(500, ErrorMessage = "The comment cannot exceed 500 characters")]
        public string Text { get; set; } = default!;

        [Required]
        public string Author { get; set; } = default!; // Comment's author

        public DateTime CreatedDate { get; set; }

        // Foreign key for the associated Post
        [Required]
        public int PostId { get; set; }
        
        public Post Post { get; set; } = default!;
    }
}
