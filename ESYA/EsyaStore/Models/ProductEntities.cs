
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using EsyaStore.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EsyaStore.Models
{
    public class ProductEntities : IdentityDbContext<ApplicationUser>
    {
        public ProductEntities() : base("SiteContext")
        {

        }
     
        public DbSet<Product> Products { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<District> District { get; set; }
        //public DbSet<ApplicationUser> ApplicationUser { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(e => e.ProductName).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Product>().Property(e => e.Price).HasColumnType("money").IsRequired();
            modelBuilder.Entity<Country>().Property(i => i.CountryName).HasMaxLength(10).IsRequired();


         
          
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}