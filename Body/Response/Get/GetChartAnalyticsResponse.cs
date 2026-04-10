namespace GlassBoard.Response.Get
{
public class GetChartAnalyticsResponse
{
    public ChartDescriptor Descriptor { get; set; }
    public ChartData Items { get; set; }
}

public class ChartDescriptor
{
    public AxisInfo XAxis { get; set; }
    public List<AxisInfo> YAxis { get; set; }
}

public class AxisInfo
{
    public string MeasurementUnit { get; set; }
    public string Label { get; set; }
}

public class ChartData
{
    public List<string> X { get; set; } // Le date
    public List<List<double>> Series { get; set; } // I valori
}
}
