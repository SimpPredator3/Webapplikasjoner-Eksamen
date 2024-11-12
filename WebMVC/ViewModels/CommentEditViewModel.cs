using System.ComponentModel.DataAnnotations;

namespace WebMVC.ViewModels
{
    public class CommentEditViewModel
    {
        [Required(ErrorMessage = "Content is required")]
        [StringLength(500, ErrorMessage = "The content cannot exceed 500 characters")]
        public string Text { get; set; } = default!;
    }
}
