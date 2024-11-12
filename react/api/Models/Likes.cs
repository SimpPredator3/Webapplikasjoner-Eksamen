using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Likes
    {
        public int LikesId { get; set; }

        public bool IsLike { get; set; }

        // Make PostId and Post nullable
        public int? PostId { get; set; }
        public Post? Post { get; set; }

        // Make UserId and User nullable
        public string? UserId { get; set; }
        public IdentityUser? User { get; set; }
    }
}