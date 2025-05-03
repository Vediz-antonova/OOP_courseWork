using AccountingForMaintenanceServicesLogic.DataLayer;

namespace AccountingForMaintenanceServicesLogic.BusinessLayer;

public class MaintenanceAddedEventArgs : EventArgs
{
    public Maintenance Maintenance { get; }
    public MaintenanceAddedEventArgs(Maintenance maintenance)
    {
        Maintenance = maintenance;
    }
}