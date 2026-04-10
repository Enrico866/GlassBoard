namespace GlassBoard.Abstractions.Config
{
    public class JobApiOptions
    {
        public string JobGenerateAndPersist { get; set; } = string.Empty;
        public string JobNotifyRefresh { get; set; } = string.Empty;
        public string AdminUser { get; set; } = string.Empty;
        public string AdminPassword { get; set; } = string.Empty;
    }
}