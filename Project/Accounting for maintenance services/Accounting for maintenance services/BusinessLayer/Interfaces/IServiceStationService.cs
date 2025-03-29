using Accounting_for_maintenance_services.DataLayer;
namespace Accounting_for_maintenance_services.BusinessLayer.Interfaces;

public interface IServiceStationService
{
    ServiceStation AddServiceStation(ServiceStation station);
    void UpdateServiceStation(int id, ServiceStation updatedStation);
    void DeleteServiceStation(int id);
    ServiceStation GetServiceStationById(int id);
    List<ServiceStation> GetAllServiceStations();
    
    List<ServiceStation> GetNearestServiceStations(decimal latitude, decimal longitude, decimal radius);
}