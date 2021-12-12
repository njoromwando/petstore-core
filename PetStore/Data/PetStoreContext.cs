using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetStore.Data.Entities;

namespace PetStore.Data
{
    public class PetStoreContext : IdentityDbContext<StoreUser>
    {
        public PetStoreContext(DbContextOptions<PetStoreContext> options)
            : base(options)
        {
           
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
        }
    }
}
