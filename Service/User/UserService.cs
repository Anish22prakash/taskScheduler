using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskSchedulerAPI.Data;
using TaskSchedulerAPI.DtoModels;
using TaskSchedulerAPI.Model;
using TaskSchedulerAPI.Common;
using TaskSchedulerAPI.Helper;

namespace TaskSchedulerAPI.Service.User
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext  _context;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(ApplicationDbContext context, ILogger<UserService> logger
            , IMapper mapper , IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<int?> createUserAsync(RegisterUserDto userDto)
        {
            try
            {
             var exUser = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == userDto.UserEmail);
                if (exUser != null)
                {
                    _logger.LogWarning($"user already exist by {exUser.UserEmail} mail id");
                    return null;
                }
               var newUser = _mapper.Map<Model.User>(userDto);
                newUser.UserRole = Convert.ToInt32(Enums.Roles.User);
                newUser.CreatedDate = DateTime.Now;
                newUser.UpdatedDate = DateTime.Now;
                var _pepper = _configuration["AppSettings:pepper"];
                var _iteration = _configuration["AppSettings:iteration"];
                newUser.PasswordSalt = PasswordHasher.GenerateSalt();
                newUser.PasswordHash = PasswordHasher.ComputeHash(userDto.password, newUser.PasswordSalt, _pepper, Convert.ToInt32(_iteration));

                await _context.Users.AddAsync(newUser);
               await _context.SaveChangesAsync();
                _logger.LogInformation($"user data is successfully save ,with userId{newUser.UserId}");
                return newUser.UserId;
            }
            catch (Exception ex)
            {
                this._logger.LogError(default(EventId), ex, "createUserAsync", userDto.ToString());
                throw;
            }
        }

        public async Task<Model.User?> getUserAsync(int id)
        {
            try
            {
                var exUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
                if (exUser == null)
                {
                    _logger.LogWarning($"no user found ny userId : {id}");
                    return null;
                }
               
                _logger.LogInformation($"user data is successfully retrived ,with userId{exUser.UserId}");
                return exUser;
            }
            catch (Exception ex)
            {
                this._logger.LogError(default(EventId), ex, "getUserAsync", id);
                throw;
            }
        }
    }
}
