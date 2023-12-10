namespace Contacts.BlazorWASM.Service
{
    public interface ITokenManager
    {
        Task<string> GetToken();

        Task<bool> SaveToken(string str);
    }
}
