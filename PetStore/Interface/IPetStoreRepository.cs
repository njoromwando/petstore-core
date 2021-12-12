using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetStore.Data.Entities;

namespace PetStore.Interface
{
   public interface IPetStoreRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();

       // Task<IEnumerable<Product>>
       IEnumerable<Product> GetProductsByCategory(string category);

        IEnumerable<Order> GetAllOrders(bool includeItems);
        IEnumerable<Order> GetOrdersByUser(string username, bool includeItems);
        Order GetOrderById(string username, int id);

        void AddEntity(object entity);
        bool SaveAll();
    }
}
