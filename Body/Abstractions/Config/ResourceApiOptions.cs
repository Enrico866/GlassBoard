namespace GlassBoard.Abstractions.Config;
public class ResourceApiOptions
{
    public string DiscoveryUrl { get; set; } = string.Empty;
    public string EndpointAddResource { get; set; } = string.Empty;
    public string Channel { get; set; } = string.Empty;
    // Aggiungi qui gli altri se ti servono nel ResourceService
}