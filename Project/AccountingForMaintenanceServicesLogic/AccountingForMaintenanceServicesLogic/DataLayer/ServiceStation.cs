
namespace AccountingForMaintenanceServicesLogic.DataLayer;

public class ServiceStation
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public decimal Rating { get; set; }
    public string ContactInfo { get; set; }
    public DateTime CreatedAt { get; set; }

    public ServiceStation(int id, string name, string address, decimal latitude, decimal longitude, decimal rating, string contactInfo)
    {
        Id = id;
        Name = name;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
        Rating = rating;
        ContactInfo = contactInfo;
        CreatedAt = DateTime.Now;
    }
}