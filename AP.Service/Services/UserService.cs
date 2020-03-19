using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AP.Core.Dtos;
using AP.Core.Services;
using AP.Service.Mapper;
using AP.Persistence.Contexts;
using AP.Domain.Models;
using System.Collections;
using System.Collections.Generic;


namespace AP.Service
{
    public class UserService : IUserService
    {
        private APContext _context;
        private UserMapper _mapper = new UserMapper();

        public UserService(APContext context)
        {
            _context = context;

        }

        public async Task<UserDto> CreateAsync(NewUser newUser)
        {
            var _user = _mapper.NewToEntity(newUser);

            _context.Add(_user);
            await _context.SaveChangesAsync();

            return _mapper.EntityToDto(_user);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var item = await _context.Users.FindAsync(id);

            if(item == null) return false;

            _context.Users.Remove(item);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<UserDto> GetByIdAsync(string id)
        {
            var item = await _context.Users.FindAsync(id);

            if (item == null) return null;

            return _mapper.EntityToDto(item);
        }
    }
}
