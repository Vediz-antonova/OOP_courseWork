using Accounting_for_maintenance_services.DataLayer;
namespace Accounting_for_maintenance_services.BusinessLayer.Interfaces;

public interface IMaintenanceService
{
    Maintenance AddMaintenance(Maintenance maintenance);
    void UpdateMaintenance(int id, Maintenance updatedMaintenance);
    void DeleteMaintenance(int id);
    Maintenance GetMaintenanceById(int id);
    List<Maintenance> GetMaintenancesByCarId(int carId);
    List<Maintenance> GetAllMaintenances();
}