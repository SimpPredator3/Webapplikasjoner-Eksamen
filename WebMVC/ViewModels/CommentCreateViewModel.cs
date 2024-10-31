using System.ComponentModel.DataAnnotations;

namespace WebMVC.ViewModels
{
    public class CommentCreateViewModel
    {
        [Required(ErrorMessage = "Content is required")]
        [StringLength(500, ErrorMessage = "The content cannot exceed 500 characters")]
        public string Content { get; set; } = default!;

        [Required]
        public int PostId { get; set; } // The post ID to which this comment belongs
    }
}
