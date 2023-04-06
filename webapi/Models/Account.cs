using Newtonsoft.Json;

namespace webapi.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Transaction> Transactions { get; set; }

    }
}
