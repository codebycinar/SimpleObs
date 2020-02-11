using System.Threading.Tasks;
using WebApi.Identity.Models;
using WebApi.Identity.ViewModels;

namespace WebUI.Utilities
{
    public partial class ApiClient
    {
        public async Task<TokenModel> Login(LoginViewModel login)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "auth/token"));
            return await GetAsync<TokenModel>(requestUrl);
        }

    }
}
