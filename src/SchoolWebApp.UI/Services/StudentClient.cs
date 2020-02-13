using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SchoolWebApp.UI.Helpers;
using Microsoft.AspNetCore.Http;
using SchoolWebApp.UI.Models;

namespace SchoolWebApp.UI.Services
{
    public interface IStudentClient
    {
        public Task<HttpResponseMessage> GetStudents(JwtModel jwtModel);
        public Task<HttpResponseMessage> GetStudentDetail(JwtModel jwtModel);
    }
    public class StudentClient : IStudentClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public StudentClient(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<HttpResponseMessage> GetStudentDetail(JwtModel jwtModel)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_config.GetValue<string>("Settings:ApiUrl"));
            httpClient.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", jwtModel.Token);
            return await httpClient.GetAsync($"api/student/detail/{jwtModel.StudentId}");
        }

        public async Task<HttpResponseMessage> GetStudents(JwtModel jwtModel)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_config.GetValue<string>("Settings:ApiUrl"));
            httpClient.DefaultRequestHeaders.Authorization
                        = new AuthenticationHeaderValue("Bearer", jwtModel.Token);
            return await httpClient.GetAsync($"api/student/");
        }
    }
}
