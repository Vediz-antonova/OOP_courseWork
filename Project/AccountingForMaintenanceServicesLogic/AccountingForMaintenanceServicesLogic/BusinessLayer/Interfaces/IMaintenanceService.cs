using AccountingForMaintenanceServicesLogic.DataLayer;

namespace AccountingForMaintenanceServicesLogic.BusinessLayer.Interfaces;

public interface IMaintenanceService
{
    Maintenance AddMaintenance(Maintenance maintenance);
    void UpdateMaintenance(int id, Maintenance updatedMaintenance);
    void DeleteMaintenance(int id);
    Maintenance GetMaintenanceById(int id);
    List<Maintenance> GetMaintenancesByCarId(int carId);
    List<Maintenance> GetAllMaintenances();
}