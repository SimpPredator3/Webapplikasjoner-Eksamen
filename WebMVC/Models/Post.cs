using System;
using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models;

public class Post
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The title is required")]
    [StringLength(100, ErrorMessage = "The title cannot exceed 100 characters")]
    public string Title { get; set; } = default!;

    [Required(ErrorMessage = "Content is required")]
    [StringLength(1000, ErrorMessage = "The content cannot exceed 1000 characters")]
    public string Content { get; set; } = default!;


    [Url(ErrorMessage = "Please enter a valid URL")]
    public string? ImageUrl { get; set; }

    public DateTime CreatedDate { get; set; }

    [Required]
    public required string Author { get; set; }
}