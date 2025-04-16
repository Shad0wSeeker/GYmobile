
namespace GYmobile.Services
{
    public interface IAuthenticationService
    {
        Task<string> GetUserIdAsync();
        Task<bool> IsAuthenticatedAsync();
    }

}
