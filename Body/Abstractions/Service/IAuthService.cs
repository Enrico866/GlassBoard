namespace GlassBoard.Abstractions.Service
{
    public interface IAuthService
    {
        Task<string> GetContextAccessTokenAsync();
        // Nuovo metodo per scenari speciali (es. admin)
        Task<string> GetAccessTokenByParamAsync(string email, string password);
    }
}
