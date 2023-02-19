using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
using System.Text.Json.Serialization;

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
        public OperandType Type { get; set; }

        [StringLength(40)]
        public string? Description { get; set; }

        [JsonIgnore]
        public DateTime Date { get; set; }

        public int WalletId { get; set; }

        [JsonIgnore]
        public Wallet? Wallet { get; set; }
    }
}
