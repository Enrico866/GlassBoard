namespace GlassBoard.Response.Get
{
public class GetJobPersistResponse
{
    public List<JobItem> Items { get; set; } = new();
}

public class JobItem
{
    public string Id { get; set; } = string.Empty;
    public string QueueName { get; set; } = string.Empty;
    public string TaskName { get; set; } = string.Empty;
    public CollectorPayload CollectorPayload { get; set; } = new();
    public PersistSchedulingPolicy SchedulingPolicy { get; set; } = new();
}

public class CollectorPayload
{
    public Resource Resource { get; set; } = new();
    public List<CollectorConfiguration> CollectorConfigurations { get; set; } = new();
}

public class Resource
{
    public string ResourceId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Dictionary<string, string> Attributes { get; set; } = new();
    public PersistResourceAccess ResourceAccess { get; set; } = new();
}

public class PersistResourceAccess
{
    public string Address { get; set; } = string.Empty;
    public InstrumentConfiguration InstrumentConfiguration { get; set; } = new();
}

public class InstrumentConfiguration
{
    public string Instrument { get; set; } = string.Empty;
}

public class CollectorConfiguration
{
    public string Name { get; set; } = string.Empty;
    public string ScalarOidTemplate { get; set; } = string.Empty;
    public List<string> ResourceIds { get; set; } = new();
}

public class PersistSchedulingPolicy
{
    public int IntervalInSeconds { get; set; }
}
}
