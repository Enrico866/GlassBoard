using GlassBoard.Response.Get;

namespace GlassBoard.Request.Add
{
    public class AddConfigurationRequest
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Instrument { get; set; }
        public string? ScalarOidTemplate { get; set; }
        public string? TableOidTemplate { get; set; }

        public Guid? CollectorId { get; set; }
        public List<ObservableDetail> Details { get; set; } = new();
    }
}
