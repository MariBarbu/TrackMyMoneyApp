using MoneyXamarin.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoneyXamarin.Services
{
    public class ApiWishService : IWishService
    {
        private readonly HttpClient _httpClient;

        public ApiWishService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Wish>> GetWishes()
        {
            var response = await _httpClient.GetAsync("Wishes");

            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<Wish>>(responseAsString);
        }

        public async Task<Wish> GetWish(Guid id)
        {
            var response = await _httpClient.GetAsync($"Wishes/{id}");

            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Wish>(responseAsString);
        }

        public async Task AddWish(Wish wish)
        {
            var response = await _httpClient.PostAsync("Wishes",
                new StringContent(JsonSerializer.Serialize(wish), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteWish(Wish wish)
        {
            var response = await _httpClient.DeleteAsync($"Books/{wish.Id}");

            response.EnsureSuccessStatusCode();
        }

       
    }
}
