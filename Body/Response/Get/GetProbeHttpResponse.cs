namespace GlassBoard.Response.Get
{
    public class ProbeListResponse
    {
        public List<ProbeItemResponse> Items { get; set; } = new();
    }

    public class ProbeItemResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string OrganizationId { get; set; }
        public string Description { get; set; }
        public string VpnIpAddress { get; set; }
    }
}