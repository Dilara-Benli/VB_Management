using System.ComponentModel.DataAnnotations;

namespace VB_api.Models
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Ad alanı gereklidir.")]
        [MaxLength(50, ErrorMessage = "Ad en fazla 50 karakter olabilir.")]
        public string name { get; set; }

        [Required(ErrorMessage = "Soyad alanı gereklidir.")]
        [MaxLength(50, ErrorMessage = "Soyad en fazla 50 karakter olabilir.")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Doğum tarihi gereklidir.")]
        public DateTime birthDate { get; set; }

        [Required(ErrorMessage = "TC Kimlik Numarası gereklidir.")]
        [Range(10000000000, 99999999999, ErrorMessage = "TC Kimlik Numarası 11 Haneli Olmalıdır.")]
        public long identityNumber { get; set; } // unique

        [Required(ErrorMessage = "E-posta alanı gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçersiz bir e-posta adresi girdiniz.")]
        [MaxLength(50, ErrorMessage = "E-posta en fazla 50 karakter olabilir.")]
        public string email { get; set; } // unique

        [RegularExpression(@"^(?=.*[!@#$%^&*(),.?\:{}|<>])[\p{L}\d!@#$%^&*(),.?\:{}|<>]{6,60}$", 
            ErrorMessage = "Şifre en az 6 karakter uzunluğunda olmalı ve bir özel karakter içermelidir.")]
        public string? password { get; set; }
    }
}
