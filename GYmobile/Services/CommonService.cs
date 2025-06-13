using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GYmobile.Dto;
using GYmobile.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using GYmobile.Filters;
using Newtonsoft.Json;
using System.Diagnostics;


namespace GYmobile.Services
{    
    public class CommonService
    {
        private readonly HttpClient _httpClient;
        public string baseUrl = "http://rentagym.runasp.net/";

        public CommonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetChatRoomDataAsync(int rentBorderId)
        {
            return await _httpClient.GetStringAsync($"/api/common/data/chatRoom?rentBorderId={rentBorderId}");
        }

        public async Task<string> GetChatHistoryAsync(string name)
        {
            return await _httpClient.GetStringAsync($"/api/common/chat/history?name={name}");
        }

        public async Task<List<string>> GetDaySchedule(int hallId, DateOnly date)
        {
            Console.WriteLine($"hallId = {hallId}, date = {date}");
            return await _httpClient.GetFromJsonAsync<List<string>>($"/api/common/day?hallId={hallId}&date={date.ToString("yyyy-MM-dd")}");
        }

        public async Task<HallDetailsRequestDTO> GetHallById(int id)
        {           
           return await _httpClient.GetFromJsonAsync<HallDetailsRequestDTO>($"/api/common/hall/{id}");
        }

        //public async Task<IEnumerable<HallListRequestDTO>> GetHallListAsync(HallListFilter filter)
        //{

        //    return await _httpClient.GetFromJsonAsync<IEnumerable<HallListRequestDTO>>("/api/common/hall/list");
        //}

        public async Task<IEnumerable<HallListRequestDTO>> GetHallListAsync(HallListFilter filter)
        {
            // Формируем query string
            var url = $"/api/common/hall/list";

            var queryParams = new List<string>();

            if (filter.TypeId > 0)
                queryParams.Add($"typeId={filter.TypeId}");

            if (filter.PaymentType)
                queryParams.Add("paymentType=true");

            if (filter.RegionId > 0)
                queryParams.Add($"regionId={filter.RegionId}");

            if (filter.SquareFrom > 0)
                queryParams.Add($"squareFrom={filter.SquareFrom}");

            if (filter.SquareTo.HasValue)
                queryParams.Add($"squareTo={filter.SquareTo.Value}");

            if (filter.PriceFrom > 0)
                queryParams.Add($"priceFrom={filter.PriceFrom}");

            if (filter.PriceTo.HasValue)
                queryParams.Add($"priceTo={filter.PriceTo.Value}");

            if (filter.Timestamp.HasValue)
                queryParams.Add($"timestamp={filter.Timestamp.Value:O}"); 

            if (filter.TimeFrom.HasValue)
                queryParams.Add($"timeFrom={filter.TimeFrom.Value:O}");

            if (filter.TimeTo.HasValue)
                queryParams.Add($"timeTo={filter.TimeTo.Value:O}");

            if (filter.OptionIds is { Count: > 0 })
            {
                foreach (var optionId in filter.OptionIds)
                {
                    queryParams.Add($"optionIds={optionId}");
                }
            }

            if (queryParams.Any())
                url += "?" + string.Join("&", queryParams);

            
            Debug.WriteLine($"[CommonService] Запрос: {url}");

            return await _httpClient.GetFromJsonAsync<IEnumerable<HallListRequestDTO>>(url);
        }

        public async Task<HallDetailsRequestDTO> GetHallByIdAsync(int hallId)
        {
            return await _httpClient.GetFromJsonAsync<HallDetailsRequestDTO>($"/api/common/hall/{hallId}");
        }

        public async Task<string> GetMonthSchedule(int hallId, DateOnly yearMonth)
        {
            Console.WriteLine($"hallId = {hallId}, yearMonth = {yearMonth}");
            return await _httpClient.GetStringAsync($"/api/common/monthSchedule?hallId={hallId}&yearMonth={yearMonth.ToString("yyyy-MM-dd")}");
        }

        public async Task<IEnumerable<HallType>> GetHallTypesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<HallType>>("/api/common/hall/types");
        }


        public async Task<string> GetUserInfoAsync(string id)
        {
            return await _httpClient.GetStringAsync($"/api/common/user/info/{id}");
        }


        public async Task<IEnumerable<Option>> GetOptionsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Option>>("api/common/options");
        }

        public async Task<string> SaveMessageAsync(ChatMessage msg)
        {
            var response = await _httpClient.PostAsJsonAsync("api/common/message", msg);
            return response.IsSuccessStatusCode ? "Message saved successfully" : "Failed to save message";
        }

        public async Task<bool> UpdatePhoneNumberAsync(UpdatePhoneNumberRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync("api/common/update-phone", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<(bool Success, string? Error)> ChangePasswordAsync(ChangePasswordRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync("api/common/change-password", request);
            if (response.IsSuccessStatusCode)
                return (true, null);

            var errorResponse = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            return (false, errorResponse?["message"]);
        }
    }

}
