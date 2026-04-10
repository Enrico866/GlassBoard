using SharedLibrary.Enum;

namespace GlassBoard.Response.Get
{
    public class SecurityItemResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        
        // Specifichiamo il tipo per sapere come trattare i dati nella UI
        public SecurityTypes SecurityType { get; set; }
        public SecretScopeTypes SecretScope { get; set; }

        // Tipicamente le API non restituiscono le password in chiaro, 
        // ma possono restituire metadati del secret (es. username, data creazione)
        public SecretDataResponse? Secret { get; set; }
    }

    public class SecretDataResponse
    {
        public string? Username { get; set; }
        public string? Token { get; set; }
        public string? Community { get; set; }
        // Aggiungi altri campi se l'API li restituisce per la visualizzazione
    }

    // Wrapper per la lista (se l'API restituisce un oggetto con una proprietà 'Items')
    public class SecurityListResponse
    {
        public List<SecurityItemResponse> Items { get; set; } = new();
    }
}