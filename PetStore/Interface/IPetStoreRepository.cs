using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetStore.Data.Entities;
using PetStore.Helpers;

namespace PetStore.Interface
{
   public interface IPetStoreRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<object> GetAllProductsPaginated(PaginationFilter filter, string route);

       // Task<IEnumerable<Product>>
        IEnumerable<Product> GetProductsByCategory(string category);

        IEnumerable<Order> GetAllOrders(bool includeItems);
        IEnumerable<Order> GetOrdersByUser(string username, bool includeItems);
        Order GetOrderById(string username, int id);

        void AddEntity(object entity);
        bool SaveAll();
    }
}
