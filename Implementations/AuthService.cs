using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Messenger.Data;
using Messenger.IServices;
using Messenger.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;



namespace Messenger.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly MessengerDbContext _messengerDb;
        private readonly IConfiguration configuration;

        public AuthService(MessengerDbContext messengerDb, IConfiguration configuration)
        {
            _messengerDb = messengerDb;
            this.configuration = configuration;
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                expires: DateTime.UtcNow.AddHours(1), // Токен будет действителен 1 час
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<User?> Register(string username, string password)
        {
            if (await _messengerDb.Users.AnyAsync(u => u.Name == username))
            {
                return null;
            }
            var user = new User()
            {
                Name = username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            };
            _messengerDb.Users.Add(user);
            await _messengerDb.SaveChangesAsync();
            return user;
        }
        public async Task<string?> Login(string username, string password)
        {
           var user = await  _messengerDb.Users.FirstOrDefaultAsync(u => u.Name == username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return null;
            }
            return GenerateJwtToken(user);

        }
    }


}
