using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WalletApi.Models
{
    public class Wallet
    {
        [Key]
        public int WalletId { get; set; }

        [Required]
        [StringLength(15)]
        public string? Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Value { get; set; }

        public ICollection<Operation>? Operations { get; set; }
    }
}
