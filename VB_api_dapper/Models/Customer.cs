﻿using System.ComponentModel.DataAnnotations;

namespace VB_api.Models
{
    public class Customer
    {
        [Key]
        public long CustomerID { get; set; } // PK, sequence 
        [Required]
        [MaxLength(50)]
        public string CustomerName { get; set; } 
        [Required]
        [MaxLength(50)]
        public string CustomerLastName { get; set; }
        [Required]
        public long CustomerIdentityNumber { get; set; } // unique
        [Required]
        public DateTime CustomerBirthDate { get; set; }
        [MaxLength(50)]
        [EmailAddress]
        public string CustomerEmail { get; set; } // unique
        [MaxLength(60)]
        [Required]
        public string CustomerPasswordHash { get; set; } // hashlenmiş şifre
    }
}
