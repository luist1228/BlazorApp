using BlazorApp.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlazorApp.States
{
    public static class DecryptJWTService
    {
        public static CustomUserClaims DecryptToken(string jwtToken)
        {
            try
            {
                if (string.IsNullOrEmpty(jwtToken)) return new CustomUserClaims();

                var handler = new JwtSecurityTokenHandler();

                var token = handler.ReadJwtToken(jwtToken);

                var email = token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Email);
                var name = token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Name);

                return new CustomUserClaims(name!.Value, email!.Value);

            }
            catch 
            {
                return null!;
            }
        }
    }
}
