using System.ComponentModel.DataAnnotations;

namespace VB_api.Models
{
    public class TransactionRequest
    {
        [Required]
        public long accountID { get; set; }

        [Required]
        //[Range(1, int.MaxValue)]
        public decimal amount { get; set; }

        [Required]
        [MaxLength(50)]
        public string explanation { get; set; }
    }
}
