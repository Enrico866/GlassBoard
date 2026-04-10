using static GlassBoard.Response.Get.GetTenantHttpResponse;

namespace GlassBoard.Abstractions.Service
{
    public interface ITenantService
    {
        Task<List<TenantItemResponse>> GetAllTenantsAsync();
    }
}