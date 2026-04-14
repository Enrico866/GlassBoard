using GlassBoard.Response;
using GlassBoard.Response.Get;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using SharedLibrary.Models;

using static SharedLibrary.Enum.Enums;

namespace GlassBoard.Abstractions.Provider
{
    public interface IResourceProvider
    {
        IReadOnlyList<ResourceApiDto> Resources { get; }
        bool IsLoaded { get; }
        Task InitializeAsync(bool forceRefresh = false);
        Task RunMultipleDiscoveriesAsync(string resourceId, List<InstrumentTypes> selectedInstruments, string orgId);
        Task ToggleMonitoringAsync(ResourceItem resource, EventCallback onUpdated);
    }
}