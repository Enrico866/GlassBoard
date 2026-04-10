namespace GlassBoard.Abstractions.Config
{
    public class ProbeApiOptions
    {
        public string ProbesUrl { get; set; } = string.Empty;
        public string AdminEmail { get; set; } = string.Empty;
        public string AdminPassword { get; set; } = string.Empty;
        public string ChannelAdmin { get; set; } = string.Empty;
    }
}