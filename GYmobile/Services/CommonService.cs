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
        /*public async Task<HallDetailsRequestDTO> GetHallById(int id)
        {
            var response = await _httpClient.GetAsync($"/api/common/hall/{id}");
            response.EnsureSuccessStatusCode(); // Проверяем, что запрос успешный
            var json = await response.Content.ReadAsStringAsync(); // Читаем JSON как строку
            return JsonConvert.DeserializeObject<HallDetailsRequestDTO>(json); // Десериализуем JSON в объект
        }*/

        public async Task<IEnumerable<HallListRequestDTO>> GetHallListAsync(HallListFilter filter)
        {
                                             
            return await _httpClient.GetFromJsonAsync<IEnumerable<HallListRequestDTO>>("/api/common/hall/list");
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

        //public async Task<object> GetOptionsAsync()
        //{
        //    return await _httpClient.GetStringAsync("api/common/options");
        //}
        public async Task<IEnumerable<Option>> GetOptionsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Option>>("api/common/options");
        }

        public async Task<string> SaveMessageAsync(ChatMessage msg)
        {
            var response = await _httpClient.PostAsJsonAsync("api/common/message", msg);
            return response.IsSuccessStatusCode ? "Message saved successfully" : "Failed to save message";
        }
    }

}
