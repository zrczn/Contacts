
using Blazored.LocalStorage;

namespace Contacts.BlazorWASM.Service
{
    public class TokenManager : ITokenManager
    {
        private readonly ILocalStorageService _localStorageService;

        public TokenManager(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task<string> GetToken()
        {
            string tkn = await _localStorageService.GetItemAsStringAsync("JWTToken");

            return string.IsNullOrEmpty(tkn) ? "0" : tkn;
        }

        public async Task<bool> SaveToken(string str)
        {
            try
            {
                await _localStorageService.SetItemAsync("JWTToken", str);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
