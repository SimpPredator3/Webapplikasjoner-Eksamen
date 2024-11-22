using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Upvote
    {
        public int UpvoteId { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string? UserId { get; set; }
        public IdentityUser? User { get; set; }
    }
}
