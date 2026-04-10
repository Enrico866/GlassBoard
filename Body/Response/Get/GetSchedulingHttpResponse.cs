using SharedLibrary.Models;

namespace GlassBoard.Response.Get
{
    public class GetSchedulingHttpResponse
    {
        public List<SchedulingPolicy> Items { get; set; } = new();
    }
}
