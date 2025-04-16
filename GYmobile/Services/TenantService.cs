using Blazored.LocalStorage;
using GYmobile.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GYmobile.Services
{
    public class TenantService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public TenantService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<string> GetTenantRentsAsync(string tenantId)
        {
            return await _httpClient.GetStringAsync($"/api/tenant/t/rents/{tenantId}");
        }

        public async Task<bool> AddReviewAsync(Review review)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/tenant/review", review);
            return response.IsSuccessStatusCode;
        }

        //public async Task<bool> RegisterRentAsync(string tenantId, int hallId, bool isRegular, string from, string to)
        //{
        //    var token = await _localStorage.GetItemAsync<string>("token");
        //    var userId = await _localStorage.GetItemAsync<string>("userId");

        //    if (string.IsNullOrEmpty(token)) return false;

        //    _httpClient.DefaultRequestHeaders.Authorization =
        //    new AuthenticationHeaderValue("Bearer", token);

        //    var requestBody = new
        //    {
        //        tenantId,
        //        hallId,
        //        isRegular,
        //        from,
        //        to
        //    };

        //    var response = await _httpClient.PostAsJsonAsync("/api/tenant/rent", requestBody);

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        var errorContent = await response.Content.ReadAsStringAsync();
        //        Console.WriteLine($"Ошибка сервера: {response.StatusCode}, Content: {errorContent}");

        //    }
        //    return response.IsSuccessStatusCode;
        //}

        public async Task<bool> RegisterRentAsync(string userId, int hallId, bool isRegular, string from, string to)
        {
            try
            {
                var token = await _localStorage.GetItemAsync<string>("token");
                if (string.IsNullOrEmpty(token)) return false;

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Формируем URL с query-параметрами
                var requestUrl = $"/api/tenant/rent?hallId={hallId}&isRegular={isRegular}&from={Uri.EscapeDataString(from)}&to={Uri.EscapeDataString(to)}";

                // Отправляем tenantId в теле запроса как строку в кавычках
                var content = new StringContent($"\"{userId}\"", Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(requestUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Ошибка сервера: {response.StatusCode}, Content: {errorContent}");
                }

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Исключение при бронировании: {ex}");
                return false;
            }
        }
    }
}