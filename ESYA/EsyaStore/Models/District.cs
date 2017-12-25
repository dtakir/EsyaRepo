using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EsyaStore.Models
{
    public class District
    {
        public int DistrictId { get; set; }
        [Display(Name = "İlçe Adı")]
        public string DistrictName { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}