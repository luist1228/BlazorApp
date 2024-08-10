using BlazorApp.DTOs;
using static BlazorApp.Responses.CustomResponses;
using System.Net.Http;
using System.Security.Policy;
using BlazorApp.States;
using System.Diagnostics;

namespace BlazorApp.Services
{
    public class AuthService
    {
        private readonly HttpClient httpClient;
        private readonly string url = "api/account";

        public AuthService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public void GetProtectedClient()
        {
            if (Constants.JWTToken == "") return;

            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers
                    .AuthenticationHeaderValue("Bearer", Constants.JWTToken);

        }

        public bool CheckIfAuthorized(HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return true;
            else return false;
        }

        public async Task<LoginResponse> GetRefreshToken()
        {
            var response = await httpClient.PostAsJsonAsync($"{url}/refresh-token", new UserSession { JWTToken = Constants.JWTToken });

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            Constants.JWTToken = result!.JWTToken;

            return result;

        }
    }
}
