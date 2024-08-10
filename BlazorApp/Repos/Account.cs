using BlazorApp.Data;
using BlazorApp.DTOs;
using BlazorApp.Models;
using BlazorApp.Responses;
using BlazorApp.States;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static BlazorApp.Responses.CustomResponses;

namespace BlazorApp.Repos
{
    public class Account : IAccount
    {
        private readonly AppDbContext appDbContext;
        private readonly IConfiguration config;

        public Account(AppDbContext appDbContext, IConfiguration config)
        {
            this.appDbContext = appDbContext;
            this.config = config; 
        }

        public async Task<LoginResponse> LoginAsync(LoginDTO model)
        {
            var findUser = await GetUser(model.Email);

            if (findUser == null) {
                return new LoginResponse(false, "Email/Password not valid");
            }

            if (!BCrypt.Net.BCrypt.Verify(model.Password, findUser.Password)) {
                return new LoginResponse(false, "Email/Password not valid");
            }

            string jwtToken = GenerateToken(findUser);


            return new LoginResponse(true, "Success", jwtToken);
        }

        public LoginResponse RefreshToken(UserSession userSession)
        {
            CustomUserClaims customUserClaims = DecryptJWTService.DecryptToken(userSession.JWTToken);
            if (customUserClaims.Email is null) return new LoginResponse(false, "Incorrect Token");

            string newToken = GenerateToken(new ApplicationUser()
            {
                Name = customUserClaims.Name,
                Email = customUserClaims.Email,
            });
            return new LoginResponse(true, "New Token", newToken);
        }

        public async Task<RegistrationResponse> RegisterAsync(RegisterDTO model)
        {
            var findUser = await GetUser(model.Email);

            if (findUser != null)
            {
                return new RegistrationResponse(false, "User already exist");
            }

            appDbContext.Users.Add(
               new ApplicationUser()
               {
                   Name = model.Name,
                   Email = model.Email,
                   Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
               });


            await appDbContext.SaveChangesAsync();

            return new RegistrationResponse(true, "Success");


        }


        private string GenerateToken(ApplicationUser user) {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.Name!),
            };

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"]!,
                audience: config["Jwt:Audience"]!,
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: credentials
                );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        
        }

        private async Task<ApplicationUser> GetUser(string email)
        {
            return await appDbContext.Users.FirstOrDefaultAsync(e => e.Email == email);
        }


    }
}
