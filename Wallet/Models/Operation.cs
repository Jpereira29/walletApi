using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WalletApi.Enums;

namespace WalletApi.Models
{
    public class Operation
    {
        [Key]
        public int OperationId { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Value { get; set; }

        [Required]
        public OperationType Type { get; set; }

        [StringLength(40)]
        public string? Description { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public int WalletId { get; set; }

        [JsonIgnore]
        public Wallet? Wallet { get; set; }
    }
}
