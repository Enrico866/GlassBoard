namespace GlassBoard.Abstractions.Config
{
    public class CollectorApiOptions
    {
        public string CollectorsUrl { get; set; } = string.Empty;
        public string ChannelAdmin { get; set; } = string.Empty;
        public string AdminUser { get; set; } = string.Empty;
        public string AdminPassword { get; set; } = string.Empty;
    }
}