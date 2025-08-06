using Data.Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities.Product;

namespace Data.Context
{
    public class MasaiContxet:DbContext
    {
        public MasaiContxet(DbContextOptions<MasaiContxet> options):base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductProperty> ProductProperties { get; set; }
    }
}
