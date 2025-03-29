using Accounting_for_maintenance_services.DataLayer;
namespace Accounting_for_maintenance_services.BusinessLayer.Interfaces;

public interface IUserService
{
    void AddUser(User user);
    void UpdateUser(int id, string newEmail, string newPassword);
    void DeleteUser(int id);
    User GetUserByEmail(string email);
    List<User> GetAllUsers();
    
    User Register(string email, string password);
    User Login(string email, string password);
}