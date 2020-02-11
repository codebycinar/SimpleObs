using Core.Data.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Utilities
{
    public partial class ApiClient
    {
        public async Task<List<StudentDetailViewModel>> GetStudents()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "student"));
            return await GetAsync<List<StudentDetailViewModel>>(requestUrl);
        }

        public async Task<StudentDetailViewModel> GetStudent(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"student/{id}"));
            return await GetAsync<StudentDetailViewModel>(requestUrl);
        }

        public async Task<List<StudentDetailViewModel>> GetStudentResults(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"student/GetStudentExamResults/{id}"));
            return await GetAsync<List<StudentDetailViewModel>>(requestUrl);
        }
    }
}
