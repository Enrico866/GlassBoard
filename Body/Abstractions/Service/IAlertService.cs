using GlassBoard.Models.Alerts;

namespace GlassBoard.Abstractions.Service
{
    public interface IAlertService
    {
        Task<List<AlertModel>> GetAlertsAsync();
    }
}
