using EsyaStore.Identity;
using EsyaStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EsyaStore.ViewModel
{
    public class UserViewModel
    {
        public List<ApplicationUser> ApplicationUser { get; set; }
        public List<Product> Product { get; set; }
    }
}