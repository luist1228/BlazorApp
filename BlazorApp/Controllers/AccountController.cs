using BlazorApp.DTOs;
using BlazorApp.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BlazorApp.Responses.CustomResponses;

namespace BlazorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccount accountrepo;

        public AccountController(IAccount accountrepo)
        {
            this.accountrepo = accountrepo;
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> RegisterAsync(RegisterDTO model)
        {
            var result = await accountrepo.RegisterAsync(model);
            return Ok(result);

        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> LoginAsync(LoginDTO model)
        {
            var result = await accountrepo.LoginAsync(model);
            return Ok(result);

        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public ActionResult<LoginResponse> RefreshToken(UserSession model)
        {
            var result = accountrepo.RefreshToken(model);
            if(result.Flag == false)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }
    }
}
