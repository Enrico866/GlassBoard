using GlassBoard.Models.Alerts;

using static MudBlazor.CategoryTypes;

namespace GlassBoard.Abstractions.Provider
{
    public interface IAlertProvider
    {
        List<AlertModel> Alerts { get; }
        bool IsLoading { get; }
        event Action OnChange;
        
        Task RefreshAlertsAsync();
    }
}
