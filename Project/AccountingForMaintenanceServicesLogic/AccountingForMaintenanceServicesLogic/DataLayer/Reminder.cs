namespace AccountingForMaintenanceServicesLogic.DataLayer;

public class Reminder
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CarId { get; set; }
    public string Type { get; set; }
    public DateTime DueDate { get; set; }
    public string Frequency { get; set; }
    public DateTime CreatedAt { get; set; }

    public Reminder(int id, int userId, int carId, string type, DateTime dueDate, string frequency)
    {
        Id = id;
        UserId = userId;
        CarId = carId;
        Type = type;
        DueDate = dueDate;
        Frequency = frequency;
        CreatedAt = DateTime.Now;
    }
}