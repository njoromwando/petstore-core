using PetStore.Data.ViewModels;
using System.Threading.Tasks;

namespace PetStore.Interface
{
    public interface IOrderService
    {
        Task<OrderViewModel> CreateOrderAsync(OrderViewModel model, string user);
    }
}
