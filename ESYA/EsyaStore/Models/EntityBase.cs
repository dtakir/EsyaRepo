using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EsyaStore.Models
{
    public class EntityBase
    {
        public EntityBase()
        {
            CreatedDate = DateTime.Now;
            IsDeleted = false;
        }

        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}