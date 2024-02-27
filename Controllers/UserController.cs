using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskSchedulerAPI.DtoModels;
using TaskSchedulerAPI.Service.User;

namespace TaskSchedulerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpPost , Route("RegisterUser")]
        public async Task<IActionResult> RegisterUser(RegisterUserDto registerUserDto)
        {
            if(ModelState.IsValid)
            {
                var data = await userService.createUserAsync(registerUserDto);
                if (data != null)
                {
                    return Ok(new { success = true, statusCode = 200, data = data });
                }
                else
                {
                    return Ok(new { success = false, statusCode = 400, error = "Failed to register user" });
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                     .Select(e => e.ErrorMessage)
                                     .ToList();

            return BadRequest(new { success = false, statusCode = 400, errors = errors });
        }

        [HttpPost, Route("GetUserById")]
        public async Task<IActionResult> GetUser(int id)
        {
            if (id > 0)
            {
                var data = await userService.getUserAsync(id);
                if (data != null)
                {
                    return Ok(new { success = true, statusCode = 200, data = data });
                }
                else
                {
                    return Ok(new { success = false, statusCode = 400, error = "Failed to retrive user"});
                }
            }
        
            return BadRequest(new { success = false, statusCode = 400, errors = $"Invalid userId {id}" });
        }
    }
}
