
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskSchedulerAPI.Common;
using TaskSchedulerAPI.Data;
using TaskSchedulerAPI.Helper;
using TaskSchedulerAPI.Model;
using TaskSchedulerAPI.Service.User;

namespace TaskSchedulerAPI.Service.Auth
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthenticateService> _logger;

        public AuthenticateService(ApplicationDbContext context , ILogger<AuthenticateService>  logger
            ,IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<(string userToken, int userId)?> Authenticate(string userEmail, string password)
        {

            var exUser = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == userEmail);
            if (exUser == null)
            {
                return null;
            }
            var pepper = _configuration["AppSettings:pepper"];
            var iteration = _configuration["AppSettings:iteration"];
            var passwordHash = PasswordHasher.ComputeHash(password, exUser.PasswordSalt, pepper, Convert.ToInt32(iteration));
            if (exUser.PasswordHash != passwordHash)
            {
                return null;
            }


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:Key"]);
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userEmail));
            if (exUser.UserRole == Convert.ToInt32(Enums.Roles.Admin))
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials =    new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            exUser.Token = tokenHandler.WriteToken(token);

            _context.Users.Update(exUser);
            await _context.SaveChangesAsync();

            return (exUser.Token, exUser.UserId);
        }
    }
}
