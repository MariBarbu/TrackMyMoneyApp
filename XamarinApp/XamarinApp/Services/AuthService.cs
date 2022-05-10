using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using XamarinApp.Models;

namespace XamarinApp.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(Register user);
        Task<string> LoginAsync(Login user);
    }
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }
        public async Task<bool> RegisterAsync(Register user)
        {
            var userRegister = new StringContent(JsonConvert.SerializeObject(user));
            userRegister.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            var response = await _httpClient.PostAsync("auth-service/account/register", userRegister);
            return response.IsSuccessStatusCode;
        }
        public async Task<string> LoginAsync(Login user)
        {
            var userLogin = new StringContent(JsonConvert.SerializeObject(user));
            userLogin.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            var response = await _httpClient.PostAsync("auth-service/account/login", userLogin);
            var result = await response.Content.ReadAsStringAsync();
            if(response.StatusCode != System.Net.HttpStatusCode.OK) return null;
            JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(result);
            var accesToken = jwtDynamic.Value<string>("accessToken");
            Debug.WriteLine(accesToken);
            response.EnsureSuccessStatusCode();
            return accesToken;
        }
    }
}
