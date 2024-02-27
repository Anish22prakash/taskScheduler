using TaskSchedulerAPI.Model;

namespace TaskSchedulerAPI.Service.Auth
{
    public interface IAuthenticateService
    {
        public Task<(string userToken, int userId)?> Authenticate(string userEmail, string password);
    }
}
