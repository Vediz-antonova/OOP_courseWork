using AccountingForMaintenanceServicesLogic.DataLayer;

namespace AccountingForMaintenanceServicesLogic.BusinessLayer.Interfaces;

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