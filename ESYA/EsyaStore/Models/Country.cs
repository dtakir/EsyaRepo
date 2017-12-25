using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EsyaStore.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        [Display(Name = "İl Adı")]
        public string CountryName { get; set; }
        public List<Product> Product { get; set; }
        public List<District> District { get; set; }

    }
}