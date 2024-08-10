using BlazorApp.DTOs;
using static BlazorApp.Responses.CustomResponses;
namespace BlazorApp.Repos
{
    public interface IAccount
    {
        Task<RegistrationResponse> RegisterAsync(RegisterDTO model);

        Task<LoginResponse> LoginAsync(LoginDTO model);

        LoginResponse RefreshToken(UserSession userSession);
    }
}
