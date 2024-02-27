using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskSchedulerAPI.Service.Auth;

namespace TaskSchedulerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;

        public AuthController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService; 
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> login(string userEmail , string password)
        {
            if (!(string.IsNullOrWhiteSpace(userEmail) && string.IsNullOrWhiteSpace(password))) {

                var data = await _authenticateService.Authenticate(userEmail, password);
                if (data == null)
                {
                    return Ok(new { success = false, statusCode = 400, error = "Invalid credentials" });
                }
                return Ok(new { success = true, statusCode = 200, data = new {Token = data.ToString() } });
            }
            return BadRequest(new { success = false, statusCode = 400, errors = "please provide valid email/password" });
        }


    }
}
