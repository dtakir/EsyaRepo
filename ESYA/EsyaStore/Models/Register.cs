using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EsyaStore.Models
{
    public class Register
    {
        [Required]

        [Display(Name = "Ad")]

        public string Name { get; set; }

        [Required]

        [Display(Name = "Soyad")]

        public string Surname { get; set; }



        [Required]

        [Display(Name = "KullanıcıAdı")]

        public string Username { get; set; }

        [Required]

        [EmailAddress]

        public string Email { get; set; }

        [Required]

        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }

        [Required]

        [Display(Name = "ŞifreTekrar")]
        [DataType(DataType.Password)]

        [Compare("PasswordHash", ErrorMessage = "Şifreler uyuşmuyor")]

        public string ConfirmPassword { get; set; }
       [Display(Name ="TelefonNo")]
       [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Display(Name = "Fotoğraf")]
        public string Photo { get; set; }
    }
}
