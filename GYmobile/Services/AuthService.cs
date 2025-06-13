using Blazored.LocalStorage;
using GYmobile.Dto;
using GYmobile.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;


namespace GYmobile.Services
{

    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<LoginResponse?> LoginAsync(LoginModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/login", model);

            if (response.IsSuccessStatusCode)
            {
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

                // Сохраняем данные пользователя
                await _localStorage.SetItemAsync("token", loginResponse.Token);
                await _localStorage.SetItemAsync("userId", loginResponse.UserId);
                await _localStorage.SetItemAsync("userRole", loginResponse.UserRole); 
                await _localStorage.SetItemAsync("email", loginResponse.Email);

                return loginResponse;
            }
            return null;
        }

        public async Task<RegisterResponse?> RegisterAsync(RegisterModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/register", model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<RegisterResponse>();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Registration failed: {errorContent}");
            }
        }

        public class RegisterResponse
        {
            public string Message { get; set; } = null!;
        }

        public async Task<string?> GetUserRoleAsync()
        {
            return await _localStorage.GetItemAsync<string>("userRole");
        }

        public async Task<string?> GetEmailAsync()
        {
            return await _localStorage.GetItemAsync<string>("email");
        }
              
    }
}
