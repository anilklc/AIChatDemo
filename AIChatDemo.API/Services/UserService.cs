using AIChatDemo.API.Context;
using AIChatDemo.API.DTOs;
using AIChatDemo.API.Interfaces;
using AIChatDemo.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AIChatDemo.API.Services
{
    public class UserService : IUserService
    {
        private readonly ChatDBContext _context;
        public UserService(ChatDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateUser(CreateUserDto user)
        {
            bool emailChehck = await _context.Users.AnyAsync(u => u.Email == user.Email);
            if (!emailChehck) 
            {
                await _context.Users.AddAsync(new()
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Password = user.Password
                });
                await _context.SaveChangesAsync();
                return true;
            }
            throw new Exception("Bu mail adresi sistemde kayıtlıdır");

        }

        public async Task<UserDto> Login(LoginUserDto loginUser)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == loginUser.Email);

            if(user == null || user.Password != loginUser.Password)
            {
                throw new Exception("Kullanıcı bilgileri hatalı");
            }

            UserDto userDto = new UserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            return userDto;
        }

    }
}
