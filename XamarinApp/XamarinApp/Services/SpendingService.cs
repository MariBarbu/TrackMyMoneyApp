using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using XamarinApp.Models;

namespace XamarinApp.Services
{
    public interface ISpendingService
    {
        Task<GetSpendings> GetSpendings(Guid categoryId);
        Task<bool> AddSpending(AddSpending spending);
        Task<bool> DeleteSpending(Guid spendingId);
        Task<AddSpending> UploadPicture(Picture picture);
    }
    public class SpendingService : ISpendingService
    {
        private readonly HttpClient _httpClient;
        public SpendingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetSpendings> GetSpendings(Guid categoryId)
        {
            var response = await _httpClient.GetAsync($"spending-service/{categoryId}");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GetSpendings>(data);
        }

        public async Task<bool> AddSpending(AddSpending spending)
        {
            var spendingToSave = new StringContent(JsonConvert.SerializeObject(spending));
            spendingToSave.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            var response = await _httpClient.PostAsync("spending-service", spendingToSave);
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }

        public async Task<AddSpending> UploadPicture(Picture picture)
        {
            
            var pictureToSave = new StringContent(JsonConvert.SerializeObject(picture));
            pictureToSave.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            var response = await _httpClient.PostAsync("spending-service/upload", pictureToSave);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AddSpending>(data);
        }

        public async Task<bool> DeleteSpending(Guid spendingId)
        {
            var response = await _httpClient.DeleteAsync($"spending-service/{spendingId}");
           
                return response.IsSuccessStatusCode;
   
        }

    }
}
