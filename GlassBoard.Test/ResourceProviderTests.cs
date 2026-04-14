using GlassBoard.Abstractions.Provider;
using GlassBoard.Abstractions.Service;
using GlassBoard.Provider;

using Moq;

using MudBlazor;

using SharedLibrary.Enum;

using System.ComponentModel.Design;

using Xunit;

using static SharedLibrary.Enum.Enums;

namespace GlassBoard.Tests;

public class ResourceProviderTests
{
    private readonly Mock<Abstractions.Service.IResourceService> _serviceMock;
    private readonly Mock<ITenantProvider> _tenantMock; // Nuova dipendenza
    private readonly Mock<IProbeProvider> _probeMock;   // Nuova dipendenza
    private readonly Mock<ISnackbar> _snackbarMock;
    private readonly ResourceProvider _provider;

    public ResourceProviderTests()
    {
        _serviceMock = new Mock<Abstractions.Service.IResourceService>();
        _tenantMock = new Mock<ITenantProvider>();
        _probeMock = new Mock<IProbeProvider>();
        _snackbarMock = new Mock<ISnackbar>();
        
        _provider = new ResourceProvider(
            _serviceMock.Object, 
            _tenantMock.Object, 
            _probeMock.Object, 
            _snackbarMock.Object);
    }

    [Fact]
    public async Task RunMultipleDiscoveriesAsync_ShouldExtractOrgIdCorrectly()
    {
        // Arrange (Dati di test)
        string resourceId = "9471dc1b-6c0e-406b-81a6-1e915018018a";
        var selectedInstruments = new List<InstrumentTypes> { InstrumentTypes.Snmp };
        var orgId = "019b21c2-108a-7f86-99cb-7f13ec159d7b";

        // Act (Esecuzione)
        await _provider.RunMultipleDiscoveriesAsync(resourceId, selectedInstruments, orgId);

        // Assert (Verifica)
        _serviceMock.Verify(s => s.RunDiscoveryAsync(
            resourceId, 
            "Snmp", 
            true,
            "019b21c2-108a-7f86-99cb-7f13ec159d7b"),
            Times.Once);
    }

    [Fact]
    public async Task RunMultipleDiscoveriesAsync_ShouldPassNullOrgId()
    {
        // Arrange
        string resourceId = "9471dc1b-6c0e-406b-81a6-1e915018018a";
        var instruments = new List<InstrumentTypes> { InstrumentTypes.Snmp };
    
        await _provider.RunMultipleDiscoveriesAsync(resourceId, instruments, null);

        // Assert
        _serviceMock.Verify(s => s.RunDiscoveryAsync(
            It.IsAny<string>(), 
            "Snmp", 
            true, 
            null), 
            Times.Once);
    }
}