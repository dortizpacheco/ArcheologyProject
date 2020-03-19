using System.Threading.Tasks;
using AP.Core.Dtos;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;

namespace AP.Core.Services
{
    public interface IUserService
    {
        Task<UserDto> GetByIdAsync(string id);
        Task<UserDto> CreateAsync(NewUser newUser);
        Task<bool> DeleteAsync(string id);

    }
}