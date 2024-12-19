using System.ComponentModel.DataAnnotations;

namespace VB_api.Models
{
    public class AccountRequest
    {
        //[Required]
        //public long customerID { get; set; } 

        [Required]
        [MaxLength(50)]
        public string accountName { get; set; }

        [Required]
        [MaxLength(50)]
        public string currencyType { get; set; }
    }
}
