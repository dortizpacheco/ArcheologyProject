using System.Threading.Tasks;
using AP.Core.Dtos;

namespace AP.Core.Services
{
    public interface IUserAuthenticationService
    {
        Task<bool> IsLoginAsync(string hash);
        Task<string> LoginAsync(string username, string password);  
    }
}