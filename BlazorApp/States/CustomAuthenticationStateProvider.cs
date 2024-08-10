using BlazorApp.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlazorApp.States
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity());

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(Constants.JWTToken))
                    return await Task.FromResult(new AuthenticationState(anonymous));

                var getUserClaims = DecryptJWTService.DecryptToken(Constants.JWTToken);
                if (getUserClaims == null)
                    return await Task.FromResult(new AuthenticationState(anonymous));

                var claimsPrincipal = SetClaimPrincipal(getUserClaims);
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));

            }
            catch {
                return await Task.FromResult(new AuthenticationState(anonymous));
            }
        }

        public void UpdateAuthenticationState(string jwtToken)
        {
            
            if (string.IsNullOrEmpty(jwtToken))
            {
                Constants.JWTToken = null!;
                return;
            }
            
            var claimsPrincipal = new ClaimsPrincipal();
            Constants.JWTToken = jwtToken;
            var getUserClaims = DecryptJWTService.DecryptToken(jwtToken);
            claimsPrincipal = SetClaimPrincipal(getUserClaims);
            
            
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public static ClaimsPrincipal SetClaimPrincipal(CustomUserClaims claims) {
            if (claims.Email is null) return new ClaimsPrincipal();

            return new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>
                {
                    new(ClaimTypes.Name, claims.Name),
                    new(ClaimTypes.Email, claims.Email)
                }
                , "JwtAuth")); 
        }

       
    }
}
