using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYmobile.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public async Task<string> GetUserIdAsync()
        {
            // Пример: получаем из SecureStorage
            return await SecureStorage.GetAsync("userId");
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            var token = await SecureStorage.GetAsync("accessToken");
            return !string.IsNullOrEmpty(token);
        }
    }

}
