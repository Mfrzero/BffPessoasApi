using bffKeepSafe.Domain.Models.Pessoas;
using bffKeepSafe.Domain.Tokens;
using Microsoft.AspNetCore.Mvc;

namespace KeepSafe.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(string username, string password)
        {
            if(username == "matheus" && password == "12345")
            {
                var token = TokenService.GenerateToken(new PessoasResponse());
                return Ok(token);
            }
            return BadRequest("username or password invalid");
        }
    }
}
