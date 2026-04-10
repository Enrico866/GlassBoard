using Microsoft.JSInterop;

namespace GlassBoard.Services
{
    public class AppContextService
    {
        public event Action OnContextChanged;

        // Connessione DB corrente (usata da Dapper)
        public string CurrentDb { get; private set; }
    
        // Credenziali correnti (usate dall'AuthService)
        public string CurrentUsername { get; private set; }
        public string CurrentPassword { get; private set; }
        public string CurrentEnvironmentName { get; private set; }
        public bool IsInitialized { get; private set; } = false;

        private readonly IConfiguration _config;
        private readonly IJSRuntime _js;
        private const string StorageKey = "selected_tenant";

        public AppContextService(IConfiguration config, IJSRuntime js)
        {
            _config = config;
            _js = js;
            // Inizializzazione di default con i valori del file config
            SetTenantInternal("VemVega"); 
        }

        /// <summary>
        /// Da chiamare nel MainLayout.OnInitializedAsync per recuperare il tenant salvato nel browser.
        /// </summary>
        public async Task InitializeAsync()
        {
            try
            {
                var savedTenant = await _js.InvokeAsync<string>("localStorage.getItem", StorageKey);
                if (!string.IsNullOrEmpty(savedTenant) && GetAvailableEnvironments().Contains(savedTenant))
                {
                    SetTenantInternal(savedTenant);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore caricamento storage: {ex.Message}");
            }
            finally
            {
                IsInitialized = true;
                OnContextChanged?.Invoke();
            }
        }

        public void SetTenant(string envName)
        {
            SetTenantInternal(envName);
            // Salvataggio "Fire and Forget" nel browser [cite: 10-02-2026]
            _js.InvokeVoidAsync("localStorage.setItem", StorageKey, envName);
            OnContextChanged?.Invoke();
        }

        private void SetTenantInternal(string envName)
        {
            var section = _config.GetSection($"Tenant:{envName}");
            if (section.Exists())
            {
                CurrentEnvironmentName = envName;
                CurrentDb = "glass_" + envName.ToLower();
                CurrentUsername = section["User"];
                CurrentPassword = section["Password"];
            }
        }

        public List<string> GetAvailableEnvironments() 
        {
            return _config.GetSection("Tenant").GetChildren().Select(x => x.Key).ToList();
        }
    }
}
