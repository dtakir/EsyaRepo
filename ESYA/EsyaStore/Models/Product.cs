using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EsyaStore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EsyaStore.Models
{
    public class Product:EntityBase
    {
        public int ProductId { get; set; }
        [Required]
        [Display(Name = "Esya Adı")]
        public string ProductName { get; set; }
        [Required]
        [Display(Name = "Fiyatı")]
        public int Price { get; set; }
   
        [Display(Name = "Fotoğraf")]
        public string Photo { get; set; }
        [Display(Name = "Açıklama")]
        public string Comment { get; set; }
        [Display(Name = "İl")]
        public int CountryId { get; set; }
        [Display(Name = "İlçe")]
        public int DistrictId { get; set; }
       
       
        public virtual Country Country { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
     
    }
}