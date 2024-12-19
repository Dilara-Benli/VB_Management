using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VB_api.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionID { get; set; } // PK, sequence
        [Required]
        public long AccountID { get; set; } // FK
        [Required]
        [MaxLength(50)]
        public string TransactionTypeName { get; set; } // FK
        [Required]
        //[Range(typeof(decimal), "0.00", "9999999999999999.99",
            //ErrorMessage = "İşlem miktarı en fazla 18 basamaklı ve 2 ondalık basamak içerebilir.")]
        public decimal TransactionAmount { get; set; } 
        [Required]
        public DateTime TransactionDate { get; set; }
        [Required]
        [MaxLength(50)]
        public string TransactionExplanation { get; set; } 
    }
}
