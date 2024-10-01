using System;
using System.ComponentModel.DataAnnotations;

namespace WebMVC1.Models;

public class Post
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow; // Set automatically

    public string? Author { get; set; } = "Anonymous"; // Default to "Anonymous"
}