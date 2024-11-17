using GYmobile.Dto;
using GYmobile.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GYmobile.Services
{
    public class LandlordService
    {
        private readonly HttpClient _httpClient;

        public LandlordService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateFacilityAsync(CreateFacilityRequestDTO facilityDto)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/landlord/facility", facilityDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateHallAsync(CreateHallRequestDTO hallDto)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/landlord/hall", hallDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Facility>> GetFacilitiesAsync(string landlordId)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Facility>>($"/api/landlord/ll/facilities/{landlordId}");
        }

        public async Task<string> GetHallRentsAsync(string landlordId)
        {
            return await _httpClient.GetStringAsync($"api/landlord/ll/rents/{landlordId}");
        }

        public async Task<IEnumerable<Hall>> GetHallsByFacilityIdAsync(int facilityId)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Hall>>($"/api/landlord/ll/halls?facilityId={facilityId}");
        }
    }
}