using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PetStore.Data.ViewModels;

namespace PetStore.Interface
{
    public interface IPetStoreAuthenticationService
    {
        Task<IdentityResult> RegisterUser(RegisterViewModel vm);
        Task<object> CreateToken(LoginViewModel model);
    }
}