using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using XamarinApp.Models;

namespace XamarinApp.Services
{
    public interface IMonthService
    {
        Task<string> UpdateBudget(UpdateBudget budget);
        Task<UpdateBudget> GetBudget();
        Task<DefaultScreen> GetDefaultScreen();
        Task<string> AddEconomy(NewEconomy economy);
    }
    public class MonthService: IMonthService
    {
        private readonly HttpClient _httpClient;
        public MonthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> UpdateBudget(UpdateBudget budget)
        {
            var budgetToUpdate = new StringContent(JsonConvert.SerializeObject(budget));
            budgetToUpdate.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            var response = await _httpClient.PostAsync("month-service/update-budget", budgetToUpdate);
            response.EnsureSuccessStatusCode();
            return response.ReasonPhrase;
        }

        public async Task<string> AddEconomy(NewEconomy economy)
        {
            var newEconomy = new StringContent(JsonConvert.SerializeObject(economy));
            newEconomy.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            var response = await _httpClient.PostAsync("month-service/add-economy", newEconomy);
            response.EnsureSuccessStatusCode();
            return response.ReasonPhrase;
        }
        public async Task<UpdateBudget> GetBudget()
        {
            var response = await _httpClient.GetAsync("month-service/budget");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UpdateBudget>(data);
        }

        public async Task<DefaultScreen> GetDefaultScreen()
        {
            var response = await _httpClient.GetAsync("month-service/default-screen");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DefaultScreen>(data);
        }
    }
}
