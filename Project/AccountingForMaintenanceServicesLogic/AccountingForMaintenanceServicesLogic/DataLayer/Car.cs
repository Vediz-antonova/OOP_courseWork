namespace AccountingForMaintenanceServicesLogic.DataLayer;

public class Car 
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public int Mileage { get; set; }
    public DateTime CreatedAt { get; set; }

    public Car(int id, int userId, string brand, string model, int year, int mileage)
    {
        Id = id;
        UserId = userId;
        Brand = brand;
        Model = model;
        Year = year;
        Mileage = mileage;
        CreatedAt = DateTime.Now;
    }
}