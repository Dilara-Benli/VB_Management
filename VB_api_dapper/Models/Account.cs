using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VB_api.Models
{
    public class Account
    {
        [Key]
        public long AccountID { get; set; } // PK, sequence
        [Required]
        public long CustomerID { get; set; } // FK
        [Required]
        [MaxLength(50)]
        public string AccountName { get; set; } 
        [Required]
        [MaxLength(50)]
        public string CurrencyType { get; set; } // FK
        [Required]
        [Range(typeof(decimal), "0.00", "9999999999999999.99", 
            ErrorMessage = "Hesap bakiyesi en fazla 18 basamaklı ve 2 ondalık basamak içerebilir.")]
        public decimal AccountBalance { get; set; } 
    }
}
