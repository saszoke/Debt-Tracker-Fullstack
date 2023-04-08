using Newtonsoft.Json;

namespace webapi.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? ProfilePicture { get; set; } = "002d36ea-21c7-4028-86ed-12674060d512_unknown.jpg";
        public List<Transaction> Transactions { get; set; }

    }
}
