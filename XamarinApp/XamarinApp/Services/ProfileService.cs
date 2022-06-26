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
    public interface IProfileService
    {
        Task<Profile> GetProfile();
        Task<string> EditProfile(Profile user);

    }
    public class ProfileService : IProfileService
    {
        private readonly HttpClient _httpClient;
        public ProfileService(HttpClient httpClient)
        {
            _httpClient=httpClient;

        }
        public async Task<Profile> GetProfile()
        {
            var response = await _httpClient.GetAsync("profile-service");
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Profile>(data);
        }

        public async Task<string> EditProfile(Profile user)
        {
            var userEdit = new StringContent(JsonConvert.SerializeObject(user));
            userEdit.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            var response = await _httpClient.PostAsync("profile-service", userEdit);
            return response.ReasonPhrase;
        }

    }
}
