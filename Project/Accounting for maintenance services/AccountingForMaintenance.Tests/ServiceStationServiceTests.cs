using Accounting_for_maintenance_services.BusinessLayer;
using Accounting_for_maintenance_services.BusinessLayer.Interfaces;
using Accounting_for_maintenance_services.DataLayer;
namespace AccountingForMaintenance.Tests;

public class ServiceStationServiceTests
{
    private readonly IServiceStationService _stationService;

    public ServiceStationServiceTests()
    {
        _stationService = new ServiceStationService();
    }

    [Fact]
    public void AddServiceStation_ValidInput_ReturnsServiceStation()
    {
        var station = new ServiceStation(0, "AutoService", "123 Street", 53.9m, 27.5m, 4.5m, "+375291234567");
        var addedStation = _stationService.AddServiceStation(station);
        Assert.NotNull(addedStation);
        Assert.Equal("AutoService", addedStation.Name);
    }
    
    [Fact]
    public void UpdateServiceStation_ValidInput_UpdatesStation()
    {
        var station = new ServiceStation(0, "AutoFix", "123 Main St", 53.9m, 27.5m, 4.5m, "+123456789");
        var addedStation = _stationService.AddServiceStation(station);
        var updatedStation = new ServiceStation(0, "FixIt", "456 Main St", 54.0m, 27.6m, 4.8m, "+987654321");

        _stationService.UpdateServiceStation(addedStation.Id, updatedStation);
        var fetchedStation = _stationService.GetServiceStationById(addedStation.Id);

        Assert.Equal("FixIt", fetchedStation.Name);
        Assert.Equal("456 Main St", fetchedStation.Address);
    }

    [Fact]
    public void DeleteServiceStation_RemovesServiceStationSuccessfully()
    {
        var station = new ServiceStation(0, "AutoService", "123 Street", 53.9m, 27.5m, 4.5m, "+375291234567");
        var addedStation = _stationService.AddServiceStation(station);
        _stationService.DeleteServiceStation(addedStation.Id);
        Assert.Throws<Exception>(() => _stationService.GetServiceStationById(addedStation.Id));
    }
    
    [Fact]
    public void GetNearestServiceStations_ReturnsStationsWithinRadius()
    {
        _stationService.AddServiceStation(new ServiceStation(0, "Station A", "Address A", 53.9m, 27.5m, 4.5m, "+111"));
        _stationService.AddServiceStation(new ServiceStation(0, "Station B", "Address B", 53.91m, 27.55m, 4.2m, "+222"));
        _stationService.AddServiceStation(new ServiceStation(0, "Station C", "Address C", 54.5m, 28.0m, 4.0m, "+333"));

        var nearestStations = _stationService.GetNearestServiceStations(53.9m, 27.5m, 10); // Радиус 10 км

        Assert.Contains(nearestStations, station => station.Name == "Station A");
        Assert.Contains(nearestStations, station => station.Name == "Station B");
        Assert.DoesNotContain(nearestStations, station => station.Name == "Station C");
    }
}