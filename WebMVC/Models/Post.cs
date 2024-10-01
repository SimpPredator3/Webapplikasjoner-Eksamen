using System;
using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models;

public class Post
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [Required]
    [StringLength(256)]
    public string Content { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime CreatedDate { get; set; }

    public string Author { get; set; }
}
