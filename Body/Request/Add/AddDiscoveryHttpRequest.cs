namespace GlassBoard.Request.Add
{
    public record AddDiscoveryHttpRequest
    {
        public string ResourceId { get; set; }
        public bool? IsPreDiscovery { get; set; } = false;
        public string InstrumentType { get; set; }
        public string OrganizationId { get; set; }
    }
}
