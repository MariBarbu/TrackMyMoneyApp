using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinApp.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http.Headers;

namespace XamarinApp.Services
{
    public interface IWishService
    {
        Task<IEnumerable<Wish>> GetWishes();
        Task<bool> AddWish(Wish wish);
        Task<bool> DeleteWish(Wish wish);
        Task<bool> ChangeStatus(Wish wish);
    }
    public class WishService : IWishService
    {
        private readonly HttpClient _httpClient;

        public WishService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }

        public async Task<IEnumerable<Wish>> GetWishes()
        {
            var response = await _httpClient.GetAsync("wish-service");

            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            try
            {
                var x = JsonConvert.DeserializeObject<List<Wish>>(data);
                return x;
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<bool> AddWish(Wish wish)
        {
            var wishToSave = new StringContent(JsonConvert.SerializeObject(wish));
            wishToSave.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            var response = await _httpClient.PostAsync("wish-service", wishToSave);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteWish(Wish wish)
        {
            var response = await _httpClient.DeleteAsync($"wish-service/{wish.Id}");

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ChangeStatus(Wish wish)
        {
            var response = await _httpClient.PostAsync($"wish-service/switch/{wish.Id}", null);

            return response.IsSuccessStatusCode;
        }
    }
}
