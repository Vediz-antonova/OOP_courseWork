using AccountingForMaintenanceServicesLogic.BusinessLayer;
using AccountingForMaintenanceServicesLogic.BusinessLayer.Interfaces;
using AccountingForMaintenanceServicesLogic.DataLayer;
namespace AccountingForMaintenanceLogic.Tests;

public class MaintenanceServiceTests
{
    private readonly IMaintenanceService _maintenanceService;

    public MaintenanceServiceTests()
    {
        _maintenanceService = new MaintenanceService();
    }

    [Fact]
    public void AddMaintenance_ValidInput_ReturnsMaintenance()
    {
        var maintenance = new Maintenance(0, 1, DateTime.Now, 15000, "Oil Change", "1234", 50, "Changed oil");
        var addedMaintenance = _maintenanceService.AddMaintenance(maintenance);
        Assert.NotNull(addedMaintenance);
        Assert.Equal("Oil Change", addedMaintenance.Category);
    }
    
    [Fact]
    public void UpdateMaintenance_ValidInput_UpdatesMaintenance()
    {
        var maintenance = new Maintenance(0, 1, DateTime.Now, 10000, "Oil Change", "1234", 50, "Initial note");
        var addedMaintenance = _maintenanceService.AddMaintenance(maintenance);
        var updatedData = new Maintenance(0, 1, DateTime.Now.AddDays(1), 11000, "Full Service", "5678", 150, "Updated note");

        _maintenanceService.UpdateMaintenance(addedMaintenance.Id, updatedData);
        var updatedMaintenance = _maintenanceService.GetMaintenanceById(addedMaintenance.Id);

        Assert.Equal("Full Service", updatedMaintenance.Category);
        Assert.Equal(11000, updatedMaintenance.Mileage);
    }

    [Fact]
    public void DeleteMaintenance_RemovesMaintenanceSuccessfully()
    {
        var maintenance = new Maintenance(0, 1, DateTime.Now, 15000, "Oil Change", "1234", 50, "Changed oil");
        var addedMaintenance = _maintenanceService.AddMaintenance(maintenance);
        _maintenanceService.DeleteMaintenance(addedMaintenance.Id);
        Assert.Throws<Exception>(() => _maintenanceService.GetMaintenanceById(addedMaintenance.Id));
    }
}