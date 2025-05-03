namespace AccountingForMaintenanceServicesLogic.DataLayer;

public class Maintenance
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public DateTime Date { get; set; }
    public int Mileage { get; set; }
    public string Category { get; set; }
    public string PartNumber { get; set; }
    public decimal Cost { get; set; }
    public string Note { get; set; }
    public DateTime CreatedAt { get; set; }

    public Maintenance(int id, int carId, DateTime date, int mileage, string category, string partNumber, decimal cost, string note)
    {
        Id = id;
        CarId = carId;
        Date = date;
        Mileage = mileage;
        Category = category;
        PartNumber = partNumber;
        Cost = cost;
        Note = note;
        CreatedAt = DateTime.Now;
    }
}