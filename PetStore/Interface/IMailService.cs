using System.Threading.Tasks;

namespace PetStore.Interface
{
    public interface IMailService
    {
        Task<bool> GMailSMTP(string reciever, string message, string subject);
    }
}
