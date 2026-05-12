using GlassBoard.Request.Add;
using GlassBoard.Response.Get;

namespace GlassBoard.Abstractions.Service
{
    public interface ICheckService
    {
        Task<List<CheckModel>> GetAllChecksAsync();

        Task<string?> AddCheckAsync(AddCheckHttpRequest request);
    }
}