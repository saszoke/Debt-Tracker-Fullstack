namespace webapi.Models
{
    public class AuthResult
    {
        public string Username { get; set; } = "";
        public bool Success { get; set; }
        public string Token { get; set; }
        public List<string> Errors { get; set; }
    }
}
