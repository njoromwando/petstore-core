using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace PetStore.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageId { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<OrderItem> Items { get; set; }

        public Product()
        {
            var rnd = new Random();
            var num = rnd.Next(100);
            Price = num;
        }
        
    }
}
