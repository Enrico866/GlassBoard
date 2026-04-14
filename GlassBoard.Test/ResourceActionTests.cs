using Bunit;

using GlassBoard.Abstractions.Provider;
using GlassBoard.Abstractions.Service;
using GlassBoard.Components.Pages.Network.Resource;
using GlassBoard.Components.Pages.Network.Resource.Check; // Il tuo namespace

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Moq;

using MudBlazor;
using MudBlazor.Services;

using SharedLibrary.Models;

using System;

namespace GlassBoard.Tests;

public class ResourceActionTests : BunitContext 
{
    [Fact]
    public async Task ClickToggleMonitoring_ShouldTriggerCallback()
    {
        // Arrange
        bool callbackCalled = false;
        var mockSnackbar = new Mock<ISnackbar>();
    
        Services.AddMudServices();
        Services.AddSingleton(mockSnackbar.Object);
        Services.AddSingleton(new Mock<IAuthService>().Object);
        Services.AddSingleton<IConfiguration>(new ConfigurationBuilder().Build());

        // Render del componente
        var cut = Render<ResourceActionButtons>(parameters => parameters
            .Add(p => p.ResourceId, "123")
            .Add(p => p.IsObserved, false)
            .Add(p => p.IsProcessing, false)
            // Passiamo una funzione fittizia che cambia una variabile quando chiamata
            .Add(p => p.OnToggleMonitoring, () => { callbackCalled = true; })
        );

        // Act
        var button = cut.Find("#btn-toggle-monitor");
        await cut.InvokeAsync(() => button.Click());

        // Assert
        // Verifichiamo che la funzione passata come parametro sia stata effettivamente eseguita
        Assert.True(callbackCalled, "La callback OnToggleMonitoring non è stata richiamata al click.");
    }

    [Fact]
    public void Button_ShouldBeDisabled_DuringProcessing()
    {
        bool callbackCalled = false;
        var mockSnackbar = new Mock<ISnackbar>();
    
        Services.AddMudServices();
        Services.AddSingleton(mockSnackbar.Object);
        Services.AddSingleton(new Mock<IAuthService>().Object);
        Services.AddSingleton<IConfiguration>(new ConfigurationBuilder().Build());

        var cut = Render<ResourceActionButtons>(p => p
            .Add(p => p.IsProcessing, true)
        );

        var button = cut.Find("#btn-toggle-monitor");
    
        // Verifica che l'attributo HTML disabled sia presente
        Assert.True(button.HasAttribute("disabled"));
    }
}