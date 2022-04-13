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
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategories>> GetCategories();
        Task<bool> AddCategoryAsync(AddCategory category);
        Task DeleteCategory(GetCategories category);
    }
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;
        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<GetCategories>> GetCategories()
        {
            var response = await _httpClient.GetAsync("category-service");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<GetCategories>>(data);
        }

        public async Task<bool> AddCategoryAsync(AddCategory category)
        {
            var categoryToSave = new StringContent(JsonConvert.SerializeObject(category));
            categoryToSave.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            var response = await _httpClient.PostAsync("category-service", categoryToSave);
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }

        public async Task DeleteCategory(GetCategories category)
        {
            var response = await _httpClient.DeleteAsync($"category-service/{category.Id}");

            response.EnsureSuccessStatusCode();
        }
    }
}
