using Application.Contracts;
using Application.DTDs;
using Exchange_data_in_secure_manner.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repo
{
    internal class UserRepo : IUser
    {
        private readonly ApplDbContext applDbContext;
        private readonly IConfiguration configuration;

        public UserRepo(ApplDbContext applDbContext, IConfiguration configuration)
        {
            this.applDbContext = applDbContext;
            this.configuration = configuration;
        }

        public async Task<LoginResponce> LoginUserAsync(LoginDTO loginDTO)
        {
            var getUser = await FindUserByEmail(loginDTO.Email);
            if (getUser == null)
                return new LoginResponce(false, "User not found, sorry");

            try
            {
                bool checkPassword = BCrypt.Net.BCrypt.Verify(loginDTO.Password, getUser.Password);
                if (checkPassword)
                    return new LoginResponce(true, "Login successful", GenerateJWTToken(getUser));
                else
                    return new LoginResponce(false, "Invalid credentials");
            }
            catch (BCrypt.Net.SaltParseException ex)
            {
                // Log the error
                Console.WriteLine($"SaltParseException: {ex.Message}");
                Console.WriteLine($"Stored hash: {getUser.Password}");
                return new LoginResponce(false, "Invalid salt format");
            }
        }

        private string GenerateJWTToken(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name!),
                new Claim(ClaimTypes.Email, user.Email!)
            };
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(5),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<ApplicationUser> FindUserByEmail(string email) =>
            await applDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<RegistrationResponse> RegisterUserAsync(RegisterUserDTO registerUserDTO)
        {
            var getUser = await FindUserByEmail(registerUserDTO.Email!);
            if (getUser != null)
                return new RegistrationResponse(false, "User already exists");

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerUserDTO.Password);

            applDbContext.Users.Add(new ApplicationUser()
            {
                Name = registerUserDTO.Name,
                Email = registerUserDTO.Email,
                Password = hashedPassword,
            });
            await applDbContext.SaveChangesAsync();
            return new RegistrationResponse(true, "Registration complete");
        }
    }
}
