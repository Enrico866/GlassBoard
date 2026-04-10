namespace GlassBoard.Response.Get
{
public class GetCollectorResponse
{
    public List<CollectorItem> Items { get; set; } = new();
}

public class CollectorItem
{
    public string Id { get; set; }
    public string TaskName { get; set; }
    public string Description { get; set; }
    public string CollectingProtocols { get; set; }
    public string QueueName { get; set; }
}
}
