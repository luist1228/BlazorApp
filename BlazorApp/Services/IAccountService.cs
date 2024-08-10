using BlazorApp.DTOs;
using static BlazorApp.Responses.CustomResponses;

namespace BlazorApp.Services
{
    public interface IAccountService
    {
        Task<RegistrationResponse> RegisterAsync(RegisterDTO model);
        Task<LoginResponse> LoginAsync(LoginDTO model);

        Task<LoginResponse> RefreshToken(UserSession model);
    }
}
