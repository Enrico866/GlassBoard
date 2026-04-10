namespace GlassBoard.Abstractions.Config;
public class TenantApiOptions
{
    public string TenantUrl { get; set; } = string.Empty;
    public string AdminEmail { get; set; } = string.Empty;
    public string AdminPassword { get; set; } = string.Empty;
    public string Channel { get; set; } = string.Empty;
}