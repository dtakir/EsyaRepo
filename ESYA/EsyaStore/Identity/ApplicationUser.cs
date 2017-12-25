
using EsyaStore.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EsyaStore.Identity
{
    public class ApplicationUser: IdentityUser
    {
        
   
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }

  

    }
}