namespace GlassBoard.Response.Get
{
    public class GetTenantHttpResponse
    {
        public class TenantListResponse
        {
            public List<TenantItemResponse> Items { get; set; } = new();
        }

        public class TenantItemResponse
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public List<OrganizationItemResponse> Organizations { get; set; } = new();
            public string DatabaseName { get; set; }
        }

        public class OrganizationItemResponse
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
    }
}
