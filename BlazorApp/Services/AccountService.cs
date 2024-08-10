using BlazorApp.DTOs;
using BlazorApp.Responses;
using BlazorApp.States;
using System.Reflection.Metadata;
using System.Security.Policy;
using static BlazorApp.Responses.CustomResponses;

namespace BlazorApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient httpClient;
        private readonly string url = "api/account";

        public AccountService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<LoginResponse> LoginAsync(LoginDTO model)
        {
            var response = await httpClient.PostAsJsonAsync($"{url}/login", model);

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return result!;
        }

        public async Task<LoginResponse> RefreshToken(UserSession userSession)
        {
            var response = await httpClient.PostAsJsonAsync($"{url}/refresh-token", userSession);
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return result!;
        }

        public void GetProtectedClient()
        {
            if (Constants.JWTToken == "") return;

            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers
                    .AuthenticationHeaderValue("Bearer", Constants.JWTToken);

        }

        public static bool CheckIfAuthorized(HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return true;
            else return false;
        }

        public async Task GetRefreshToken()
        {
            var response = await httpClient.PostAsJsonAsync($"{url}/refresh-token", new UserSession { JWTToken = Constants.JWTToken });
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            Constants.JWTToken = result!.JWTToken;

        }

        public async Task<RegistrationResponse> RegisterAsync(RegisterDTO model)
        {
            var response = await httpClient.PostAsJsonAsync($"{url}/register", model);
            var result = await response.Content.ReadFromJsonAsync<RegistrationResponse>();
            return result!;
        }
    }
}
