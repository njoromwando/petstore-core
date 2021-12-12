using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using PetStore.Data;
using PetStore.Data.Entities;
using PetStore.Interface;

namespace PetStore.Repository
{
    public class PetStoreRepository : IPetStoreRepository
    {
        private readonly PetStoreContext _context;
        private readonly ILogger<PetStoreRepository> _logger;

        public PetStoreRepository(PetStoreContext context, ILogger<PetStoreRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public void AddEntity(object entity)
        {
            _context.Add(entity);
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if (includeItems)
            {
                return _context.Orders
                  .Include(o => o.Items)
                  .ThenInclude(i => i.Product)
                  .ToList();
            }
            else
            {
                return _context.Orders
                  .ToList();
            }
        }

        public async  Task<IEnumerable<Product>>  GetAllProducts()
        {
            try
            {
                _logger.LogInformation("GetAllProducts was called...");

                return await _context.Products
                    .OrderBy(p => p.Title)
                    .ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
        }

        public Order GetOrderById(string username, int id)
        {
            return _context.Orders
        .Include(o => o.Items)
        .ThenInclude(i => i.Product)
        .Where(o => o.Id == id && o.User.UserName == username)
        .FirstOrDefault();
        }

        public IEnumerable<Order> GetOrdersByUser(string username, bool includeItems)
        {
            if (includeItems)
            {
                return _context.Orders
                  .Include(o => o.Items)
                  .ThenInclude(i => i.Product)
                  .Where(o => o.User.UserName == username)
                  .ToList();
            }
            else
            {
                return _context.Orders
                  .Where(o => o.User.UserName == username)
                  .ToList();
            }
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            throw new NotImplementedException();
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
