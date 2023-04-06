using Microsoft.Build.Framework;

namespace webapi.Models.DTOs
{
    public class UserLoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
