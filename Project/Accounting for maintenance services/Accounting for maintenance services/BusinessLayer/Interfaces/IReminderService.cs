using Accounting_for_maintenance_services.DataLayer;
namespace Accounting_for_maintenance_services.BusinessLayer.Interfaces;

public interface IReminderService
{
    Reminder CreateReminder(Reminder reminder);
    void UpdateReminder(int id, Reminder updatedReminder);
    void DeleteReminder(int id);
    Reminder GetReminderById(int id);
    List<Reminder> GetRemindersByUserId(int userId);
    List<Reminder> GetRemindersByCarId(int carId);
    List<Reminder> GetAllReminders();
}