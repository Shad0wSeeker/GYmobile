using Blazored.LocalStorage;
using GYmobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GYmobile.Services
{
    /* public class AuthService
     {
         private readonly HttpClient _httpClient;

         public AuthService(HttpClient httpClient)
         {
             _httpClient = httpClient;
         }

         public async Task<string?> LoginAsync(string username, string password)
         {
             var loginDto = new { Username = username, Password = password };
             var response = await _httpClient.PostAsJsonAsync("/api/auth/login", loginDto);
             if (response.IsSuccessStatusCode)
             {
                 var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
                 return result?.Token;
             }
             return null;
         }
     }

     public class TokenResponse
     {
         public string Token { get; set; }
     }*/



    //public class AuthService
    //{
    //    private readonly HttpClient _httpClient;

    //    public AuthService(HttpClient httpClient)
    //    {
    //        _httpClient = httpClient;
    //    }

    //    public async Task<LoginResponse?> LoginAsync(LoginModel model)
    //    {
    //        var response = await _httpClient.PostAsJsonAsync("api/Auth/login", model);

    //        if (response.IsSuccessStatusCode)
    //        {
    //            return await response.Content.ReadFromJsonAsync<LoginResponse>();
    //        }

    //        return null;
    //    }
    //}


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
                await _localStorage.SetItemAsync("userRole", loginResponse.UserRole); // Сохраняем роль

                return loginResponse;
            }

            return null;
        }

        public async Task<string?> GetUserRoleAsync()
        {
            return await _localStorage.GetItemAsync<string>("userRole");
        }
    }

}
