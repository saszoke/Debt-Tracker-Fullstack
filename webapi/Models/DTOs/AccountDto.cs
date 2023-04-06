using Microsoft.Build.Framework;

namespace webapi.Models.DTOs
{
    public class AccountDto
    {
        [Required]
        public string Name { get; set; }
    }
}
