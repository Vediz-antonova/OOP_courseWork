using AccountingForMaintenanceServicesLogic.BusinessLayer.Interfaces;
using AccountingForMaintenanceServicesLogic.DataLayer;

namespace AccountingForMaintenanceServicesLogic.BusinessLayer;

public class MaintenanceService : IMaintenanceService
{
    private readonly List<Maintenance> _maintenances = new List<Maintenance>();
    private int _nextMaintenanceId = 1; 
    
    public event EventHandler<MaintenanceAddedEventArgs> MaintenanceAdded;
    protected virtual void OnMaintenanceAdded(Maintenance maintenance)
    {
        MaintenanceAdded?.Invoke(this, new MaintenanceAddedEventArgs(maintenance));
    }
    public Maintenance AddMaintenance(Maintenance maintenance)
    {
        if (maintenance.Date == default)
            throw new ArgumentException("Дата проведения работ обязательна.");
        if (maintenance.Mileage < 0)
            throw new ArgumentException("Пробег не может быть отрицательным.");
        if (string.IsNullOrWhiteSpace(maintenance.Category))
            throw new ArgumentException("Категория обслуживания обязательна.");

        maintenance.Id = _nextMaintenanceId++;
        _maintenances.Add(maintenance);
        Console.WriteLine("Запись обслуживания добавлена.");
        
        OnMaintenanceAdded(maintenance);
        return maintenance;
    }
    public void UpdateMaintenance(int id, Maintenance updatedMaintenance)
    {
        var maintenance = _maintenances.FirstOrDefault(m => m.Id == id);
        if (maintenance == null)
            throw new Exception("Запись обслуживания не найдена.");

        maintenance.Date = updatedMaintenance.Date;
        maintenance.Mileage = updatedMaintenance.Mileage;
        maintenance.Category = updatedMaintenance.Category;
        maintenance.PartNumber = updatedMaintenance.PartNumber;
        maintenance.Cost = updatedMaintenance.Cost;
        maintenance.Note = updatedMaintenance.Note;

        Console.WriteLine("Запись обслуживания обновлена.");
        OnMaintenanceAdded(maintenance);
    }
    public void DeleteMaintenance(int id)
    {
        var maintenance = _maintenances.FirstOrDefault(m => m.Id == id);
        if (maintenance == null)
            throw new Exception("Запись обслуживания не найдена.");

        _maintenances.Remove(maintenance);
        Console.WriteLine("Запись обслуживания удалена.");
    }
    public Maintenance GetMaintenanceById(int id)
    {
        var maintenance = _maintenances.FirstOrDefault(m => m.Id == id);
        return maintenance ?? throw new Exception("Запись обслуживания не найдена.");
    }
    public List<Maintenance> GetMaintenancesByCarId(int carId)
    {
        return _maintenances.Where(m => m.CarId == carId).ToList();
    }
    public List<Maintenance> GetAllMaintenances()
    {
        return _maintenances;
    }
}