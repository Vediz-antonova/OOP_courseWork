using AccountingForMaintenanceServicesLogic.BusinessLayer.Interfaces;
using AccountingForMaintenanceServicesLogic.DataLayer;

namespace AccountingForMaintenanceServicesLogic.BusinessLayer;

public class ReminderService : IReminderService
{
    private readonly List<Reminder> _reminders = new List<Reminder>();
    private int _nextReminderId = 1;
    
    public Reminder CreateReminder(Reminder reminder)
    {
        if (string.IsNullOrWhiteSpace(reminder.Type))
            throw new ArgumentException("Тип напоминания обязателен.");
        if (reminder.DueDate == default)
            throw new ArgumentException("Дата напоминания недопустима.");
        if (string.IsNullOrWhiteSpace(reminder.Frequency))
            throw new ArgumentException("Частота напоминания обязателена.");

        reminder.Id = _nextReminderId++;
        _reminders.Add(reminder);
        Console.WriteLine("Напоминание создано успешно.");
        return reminder;
    }
    public void UpdateReminder(int id, Reminder updatedReminder)
    {
        var reminder = _reminders.FirstOrDefault(r => r.Id == id);
        if (reminder == null)
            throw new Exception("Напоминание не найдено.");

        reminder.Type = updatedReminder.Type;
        reminder.DueDate = updatedReminder.DueDate;
        reminder.Frequency = updatedReminder.Frequency;

        Console.WriteLine("Напоминание обновлено успешно.");
    }
    public void DeleteReminder(int id)
    {
        var reminder = _reminders.FirstOrDefault(r => r.Id == id);
        if (reminder == null)
            throw new Exception("Напоминание не найдено.");

        _reminders.Remove(reminder);
        Console.WriteLine("Напоминание удалено успешно.");
    }
    public Reminder GetReminderById(int id)
    {
        var reminder = _reminders.FirstOrDefault(r => r.Id == id);
        return reminder ?? throw new Exception("Напоминание не найдено.");
    }
    public List<Reminder> GetRemindersByUserId(int userId)
    {
        return _reminders.Where(r => r.UserId == userId).ToList();
    }
    public List<Reminder> GetRemindersByCarId(int carId)
    {
        return _reminders.Where(r => r.CarId == carId).ToList();
    }
    public List<Reminder> GetAllReminders()
    {
        return _reminders;
    }
}