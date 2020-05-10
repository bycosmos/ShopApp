using Microsoft.EntityFrameworkCore;
using SiparisApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SiparisApp.Dal.Concrete.EfCore
{
    public class ShopContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=SiparisAppDb;integrated security=true;");
        }

        public DbSet<Product> Products { get; set; }
       
        public DbSet<Basket> Baskets { get; set; }

        public DbSet<Order> Orders { get; set; }


     


    }
}
