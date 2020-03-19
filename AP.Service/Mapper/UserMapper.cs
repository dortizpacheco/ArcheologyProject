using System;
using System.Collections.Generic;
using AP.Core.Dtos;
using AP.Domain.Models;

namespace AP.Service.Mapper
{
    public class UserMapper
    {
        public UserDto EntityToDto(User enty) => new UserDto()
        {   
            Id = enty.Id,
            FirstName = enty.FirstName,
            LastName = enty.LastName,
        };
        public User NewToEntity(NewUser newEnty) => new User()
        {
            Username = newEnty.Username,
            Password = newEnty.Password,
            FirstName = newEnty.FirstName,
            LastName = newEnty.LastName,
        };
    }
}