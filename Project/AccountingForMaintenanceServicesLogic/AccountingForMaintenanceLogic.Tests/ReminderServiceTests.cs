using AccountingForMaintenanceServicesLogic.BusinessLayer;
using AccountingForMaintenanceServicesLogic.BusinessLayer.Interfaces;
using AccountingForMaintenanceServicesLogic.DataLayer;
namespace AccountingForMaintenanceLogic.Tests;

public class ReminderServiceTests
{
    private readonly IReminderService _reminderService;

    public ReminderServiceTests()
    {
        _reminderService = new ReminderService();
    }

    [Fact]
    public void CreateReminder_ValidInput_ReturnsReminder()
    {
        var reminder = new Reminder(0, 1, 1, "Oil Change", DateTime.Now.AddDays(30), "Monthly");
        var addedReminder = _reminderService.CreateReminder(reminder);
        Assert.NotNull(addedReminder);
        Assert.Equal("Oil Change", addedReminder.Type);
    }
    
    [Fact]
    public void UpdateReminder_ValidInput_UpdatesReminder()
    {
        var reminder = new Reminder(0, 1, 1, "Tire Check", DateTime.Now.AddDays(30), "Monthly");
        var addedReminder = _reminderService.CreateReminder(reminder);
        var updatedReminder = new Reminder(0, 1, 1, "Brake Check", DateTime.Now.AddDays(60), "Yearly");

        _reminderService.UpdateReminder(addedReminder.Id, updatedReminder);
        var fetchedReminder = _reminderService.GetReminderById(addedReminder.Id);

        Assert.Equal("Brake Check", fetchedReminder.Type);
        Assert.Equal("Yearly", fetchedReminder.Frequency);
    }

    [Fact]
    public void DeleteReminder_RemovesReminderSuccessfully()
    {
        var reminder = new Reminder(0, 1, 1, "Oil Change", DateTime.Now.AddDays(30), "Monthly");
        var addedReminder = _reminderService.CreateReminder(reminder);
        _reminderService.DeleteReminder(addedReminder.Id);
        Assert.Throws<Exception>(() => _reminderService.GetReminderById(addedReminder.Id));
    }
}