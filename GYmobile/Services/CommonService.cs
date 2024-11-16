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

namespace GYmobile.Services
{    
    public class CommonService
    {
        private readonly HttpClient _httpClient;

        public CommonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetChatRoomDataAsync(int rentBorderId)
        {
            return await _httpClient.GetStringAsync($"api/common/data/chatRoom?rentBorderId={rentBorderId}");
        }

        public async Task<string> GetChatHistoryAsync(string name)
        {
            return await _httpClient.GetStringAsync($"api/common/chat/history?name={name}");
        }

        public async Task<object> GetDaySchedule(int hallId, DateOnly date)
        {
            return await _httpClient.GetFromJsonAsync<object>($"api/common/day?hallId={hallId}&date={date}");
        }

        public async Task<object> GetHallById(int id)
        {
            return await _httpClient.GetFromJsonAsync<object>($"swagger/api/common/hall/{id}");
        }

        public async Task<IEnumerable<HallListRequestDTO>> GetHallListAsync(HallListFilter filter)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<HallListRequestDTO>>("swagger/api/common/hall/list");
        }

        public async Task<string> GetMonthSchedule(int hallId, DateOnly yearMonth)
        {
            return await _httpClient.GetStringAsync($"swagger/api/common/monthSchedule?hallId={hallId}&yearMonth={yearMonth}");
        }

        public async Task<IEnumerable<string>> GetHallTypesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<string>>("swagger/api/common/hall/types");
        }

        public async Task<string> GetUserInfoAsync(string id)
        {
            return await _httpClient.GetStringAsync($"swagger/api/common/user/info/{id}");
        }

        public async Task<object> GetOptionsAsync()
        {
            return await _httpClient.GetStringAsync("api/common/options");
        }

        public async Task<string> SaveMessageAsync(ChatMessage msg)
        {
            var response = await _httpClient.PostAsJsonAsync("api/common/message", msg);
            return response.IsSuccessStatusCode ? "Message saved successfully" : "Failed to save message";
        }
    }

}
