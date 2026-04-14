using System.Net;
using MudBlazor;
using Microsoft.Extensions.DependencyInjection; // Serve per IServiceProvider

public class HttpInterceptorHandler : DelegatingHandler
{
    private readonly IServiceProvider _serviceProvider;

    // Iniettiamo IServiceProvider invece di ISnackbar direttamente
    public HttpInterceptorHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                // Recuperiamo lo snackbar solo nel momento in cui serve davvero (on-demand)
                var snackbar = _serviceProvider.GetRequiredService<ISnackbar>();
                
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        snackbar.Add("Sessione scaduta.", Severity.Error);
                        break;
                    case HttpStatusCode.InternalServerError:
                        snackbar.Add("Errore 500 del server.", Severity.Error);
                        break;
                    default:
                        snackbar.Add($"Errore: {response.StatusCode}", Severity.Warning);
                        break;
                }
            }
            return response;
        }
        catch (Exception)
        {
            // Recuperiamo lo snackbar anche qui on-demand
            var snackbar = _serviceProvider.GetRequiredService<ISnackbar>();
            snackbar.Add("Server non raggiungibile.", Severity.Error);
            throw;
        }
    }
}