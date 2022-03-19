using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinApp.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;

namespace XamarinApp.Services
{
    public interface IWishService
    {
        Task<IEnumerable<Wish>> GetWishes();
        Task<Wish> GetWish(Guid id);
        //Task AddWish(Wish wish);
        Task DeleteWish(Wish wish);
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
            var response = await _httpClient.GetAsync("wish-service/all");

            response.EnsureSuccessStatusCode();
            var contentStream = await response.Content.ReadAsStreamAsync();
            var streamReader = new StreamReader(contentStream);
            var jsonReader = new JsonTextReader(streamReader);

            //var responseAsString = await response.Content.ReadAsStringAsync();
            JsonSerializer serializer = new JsonSerializer();

            return serializer.Deserialize<IEnumerable<Wish>>(jsonReader);
        }

        public async Task<Wish> GetWish(Guid id)
        {
            var response = await _httpClient.GetAsync($"wish-service/{id}");

            response.EnsureSuccessStatusCode();
            var contentStream = await response.Content.ReadAsStreamAsync();
            var streamReader = new StreamReader(contentStream);
            var jsonReader = new JsonTextReader(streamReader);

            JsonSerializer serializer = new JsonSerializer();
            //var responseAsString = await response.Content.ReadAsStringAsync();
            return serializer.Deserialize<Wish>(jsonReader);
        }

        //public async Task AddWish(Wish wish)
        //{
        //    JsonSerializer serializer = new JsonSerializer();
        //    TextWriter writer = new TextWriter('json.txt');
        //        var response = await _httpClient.PostAsync("wish-service",
        //        new StringContent(serializer.Serialize(, wish), Encoding.UTF8, "application/json"));

        //    response.EnsureSuccessStatusCode();
        //}

        public async Task DeleteWish(Wish wish)
        {
            var response = await _httpClient.DeleteAsync($"wish-service/{wish.Id}");

            response.EnsureSuccessStatusCode();
        }
    }
}
