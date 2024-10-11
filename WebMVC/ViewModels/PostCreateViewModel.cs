using Microsoft.AspNetCore.Mvc.Rendering;
using WebMVC.Models;
using System.ComponentModel.DataAnnotations;

namespace WebMVC.ViewModels;

public class PostCreateViewModel
{
    [Required]
    [StringLength(30)]
    public string Title { get; set; } = default!;

    [Required]
    [StringLength(1000)]
    public string Content { get; set; } = default!;

    [Required]
    [Url]
    public string ImageUrl { get; set; } = default!;
   
    [Required]
    [StringLength(30)]
    public string Tag { get; set; } = default!;
}