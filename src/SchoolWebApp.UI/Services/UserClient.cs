using Microsoft.Extensions.Configuration;
using Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SchoolWebApp.UI.Services
{
    public interface IUserClient
    {
        public Task<HttpResponseMessage> Login(LoginViewModel login);
    }
    public class UserClient : IUserClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public UserClient(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<HttpResponseMessage> Login(LoginViewModel login)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_config.GetValue<string>("Settings:ApiUrl"));
            string stringData = JsonConvert.SerializeObject(login);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            httpClient.DefaultRequestHeaders.Accept.Add(contentType);

            return await httpClient.PostAsync("api/auth/token", contentData);
        }
    }
}
