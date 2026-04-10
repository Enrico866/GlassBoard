using GlassBoard.Request.Add;
using GlassBoard.Response.Get;

namespace GlassBoard.Abstractions.Service
{
    public interface ISecurityService
    {
        Task<bool> CreateSecurityConfigAsync(AddSecurityRequest request);
        Task<List<SecurityItemResponse>> GetAllSecuritiesAsync(); // Nuovo metodo
    }
}