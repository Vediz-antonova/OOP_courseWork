using Accounting_for_maintenance_services.BusinessLayer.Interfaces;
using Accounting_for_maintenance_services.DataLayer;
namespace Accounting_for_maintenance_services.BusinessLayer;

public class ServiceStationService : IServiceStationService
{
    private readonly List<ServiceStation> _stations = new List<ServiceStation>();
    private int _nextStationId = 1;
    
    public ServiceStation AddServiceStation(ServiceStation station)
    {
        if (string.IsNullOrWhiteSpace(station.Name))
            throw new ArgumentException("Название СТО обязательно.");
        if (string.IsNullOrWhiteSpace(station.Address))
            throw new ArgumentException("Адрес СТО обязателен.");

        station.Id = _nextStationId++;
        _stations.Add(station);
        Console.WriteLine("СТО успешно добавлено.");
        return station;
    }
    public void UpdateServiceStation(int id, ServiceStation updatedStation)
    {
        var station = GetServiceStationById(id);
        if (station == null)
            throw new Exception("СТО не найдено.");

        station.Name = updatedStation.Name;
        station.Address = updatedStation.Address;
        station.Latitude = updatedStation.Latitude;
        station.Longitude = updatedStation.Longitude;
        station.Rating = updatedStation.Rating;
        station.ContactInfo = updatedStation.ContactInfo;

        Console.WriteLine("Информация о СТО обновлена.");
    }
    public void DeleteServiceStation(int id)
    {
        var station = GetServiceStationById(id);
        if (station == null)
            throw new Exception("СТО не найдено.");

        _stations.Remove(station);
        Console.WriteLine("СТО удалено.");
    }
    public ServiceStation GetServiceStationById(int id)
    {
        var station = _stations.FirstOrDefault(s => s.Id == id);
        return station ?? throw new Exception("СТО не найдено.");
    }
    public List<ServiceStation> GetAllServiceStations()
    {
        return _stations;
    }
    
    public List<ServiceStation> GetNearestServiceStations(decimal latitude, decimal longitude, decimal radius)
    {
        List<ServiceStation> nearestStations = new List<ServiceStation>();
        foreach (var station in _stations)
        {
            double distance = CalculateDistance((double)latitude, (double)longitude, (double)station.Latitude, (double)station.Longitude);
            if (distance <= (double)radius)
                nearestStations.Add(station);
        }
        return nearestStations;
    }
    private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        const double EarthRadiusKm = 6371; // Радиус Земли
        double dLat = DegreesToRadians(lat2 - lat1);
        double dLon = DegreesToRadians(lon2 - lon1);
        double lat1Rad = DegreesToRadians(lat1);
        double lat2Rad = DegreesToRadians(lat2);

        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1Rad) * Math.Cos(lat2Rad);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return EarthRadiusKm * c;
    }
    private double DegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180;
    }
}