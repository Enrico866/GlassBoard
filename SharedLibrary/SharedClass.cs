/* Shared classes can be referenced by both the Client and Server */

namespace SharedLibrary
{
    
    public class CollectionProfileResponse
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int? TotalCount { get; set; }
        public List<CollectionProfileItem> Items { get; set; } = new();
    }

    public class CollectionProfileItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public string SchedulingPolicyId { get; set; }
        public string ResourceTypes { get; set; }
    }
}

