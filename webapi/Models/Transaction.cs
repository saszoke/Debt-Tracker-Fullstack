using System.Text.Json.Serialization;

namespace webapi.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [JsonIgnore]
        public Account Account { get; set; }
        public int AccountId { get; set; }

        public int Amount { get; set; }

        public bool IsIncoming { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
