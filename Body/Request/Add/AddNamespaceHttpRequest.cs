namespace GlassBoard.Request.Add
{
public class AddNamespaceHttpRequest
    {
        public string? Id { get; set; } // Null per creazione, valorizzato per modifica
        public string Name { get; set; } = string.Empty;
        public List<string> ObservableConfigurationIds { get; set; } = new();
    }
}
