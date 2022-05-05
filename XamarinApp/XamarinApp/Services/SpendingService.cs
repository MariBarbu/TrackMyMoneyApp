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
        void SavePicture(string name, Stream data);
        Task<AddSpending> UploadPicture(byte[] pictureArray);
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

        public async Task<AddSpending> UploadPicture(byte[] pictureArray)
        {
            var picture = new Picture { Image = pictureArray };
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
            try
            {
                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
            
              
        }

        public void SavePicture(string name, Stream data)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            documentsPath = Path.Combine(documentsPath, "Pictures");
            Directory.CreateDirectory(documentsPath);

            string filePath = Path.Combine(documentsPath, name);

            byte[] bArray = new byte[data.Length];
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                using (data)
                {
                    data.Read(bArray, 0, (int)data.Length);
                }
                int length = bArray.Length;
                fs.Write(bArray, 0, length);               
            }
           
        }
    }
}
