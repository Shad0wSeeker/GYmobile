using GYmobile.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GYmobile.Services
{
    public class TenantService
    {
        private readonly HttpClient _httpClient;

        public TenantService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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

        public async Task<bool> RegisterRentAsync(string tenantId, int hallId, bool isRegular, DateTime from, DateTime to)
        {
            var requestBody = new
            {
                tenantId,
                hallId,
                isRegular,
                from,
                to
            };
            var response = await _httpClient.PostAsJsonAsync("/api/tenant/rent", requestBody);
            return response.IsSuccessStatusCode;
        }
    }
}