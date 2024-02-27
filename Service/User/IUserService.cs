using TaskSchedulerAPI.Model;
using TaskSchedulerAPI.DtoModels;

namespace TaskSchedulerAPI.Service.User
{
    public interface IUserService
    {
        public Task<int?> createUserAsync(RegisterUserDto userDto);
        public Task<Model.User?> getUserAsync(int id);
    }
}
