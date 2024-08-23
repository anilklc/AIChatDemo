using AIChatDemo.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AIChatDemo.API.Interfaces
{
    public interface IUserService
    {
        public Task<bool> CreateUser(CreateUserDto user);

        public Task<UserDto> Login(LoginUserDto loginUser);
       
    }
}
