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
    public interface IAuthService
    {
        Task RegisterAsync(Register user);
    }
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }
        public async Task RegisterAsync(Register user)
        {
            var userRegister = new StringContent(JsonConvert.SerializeObject(user));
            userRegister.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            var response = await _httpClient.PostAsync("auth-service/account/register", userRegister);
           response.EnsureSuccessStatusCode();
        }
    }
}
