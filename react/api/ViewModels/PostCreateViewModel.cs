using System.ComponentModel.DataAnnotations;

namespace api.ViewModels
{
    public class PostCreateViewModel
    {
        [Required]
        [StringLength(30)]
        public string Title { get; set; } = default!;

        [Required]
        [StringLength(1000)]
        public string Content { get; set; } = default!;

        [Url]
        public string? ImageUrl { get; set; } = default!;

        [StringLength(30)]
        public string? Tag { get; set; } = default!;
    }
}