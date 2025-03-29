using Accounting_for_maintenance_services.DataLayer;
namespace Accounting_for_maintenance_services.BusinessLayer;

public class MaintenanceAddedEventArgs : EventArgs
{
    public Maintenance Maintenance { get; }
    public MaintenanceAddedEventArgs(Maintenance maintenance)
    {
        Maintenance = maintenance;
    }
}