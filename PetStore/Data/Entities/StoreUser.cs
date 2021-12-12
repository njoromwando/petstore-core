using Microsoft.AspNetCore.Identity;

namespace PetStore.Data.Entities
{
    public class StoreUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}