using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace webapi.Models.DTOs
{
    public class TransactionDto
    {
        public int? TransactionId { get; set; }
        public int AccountId { get; set; } = 1;
        [Range(1000, 2147483647)]
        public int Amount { get; set; }

        public bool IsIncoming { get; set; } = true;

        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    }
}
