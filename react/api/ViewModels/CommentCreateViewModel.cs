using System.ComponentModel.DataAnnotations;

namespace api.ViewModels
{
    public class CommentCreateViewModel
    {
        [Required(ErrorMessage = "Comment cannot be empty")]
        [StringLength(500, ErrorMessage = "The comment cannot exceed 500 characters")]
        public string Text { get; set; } = default!;
    }
}