using Accounting_for_maintenance_services.DataLayer;
using Accounting_for_maintenance_services.BusinessLayer.Interfaces;
namespace Accounting_for_maintenance_services.BusinessLayer;

public class UserService: IUserService
{
    private readonly List<User> _users = new List<User>();
    private int _nextUserId = 1; 

    public void AddUser(User user)
    {
        if (_users.Any(u => u.Email == user.Email))
        {
            throw new Exception("Пользователь с таким email уже существует.");
        }

        _users.Add(user);
    }
    public void UpdateUser(int id, string newEmail, string newPassword)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            throw new Exception("Пользователь не найден.");

        if (string.IsNullOrWhiteSpace(newEmail))
            throw new ArgumentException("Email обязателен.");
        if (_users.Any(u => u.Email.Equals(newEmail, StringComparison.OrdinalIgnoreCase) && u.Id != id))
            throw new Exception("Пользователь с таким email уже существует.");

        if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 8)
            throw new ArgumentException("Пароль должен содержать не менее 8 символов.");

        user.Email = newEmail;
        user.Password = newPassword;

        Console.WriteLine("Данные пользователя успешно обновлены.");
    }
    public void DeleteUser(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            _users.Remove(user);
        }
        else
        {
            throw new Exception("Пользователь не найден.");
        }
    }
    public User GetUserByEmail(string email)
    {
        return _users.FirstOrDefault(u => u.Email == email);
    }
    public List<User> GetAllUsers()
    {
        return _users;
    }
    
    public User Register(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email обязателен.");
        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            throw new ArgumentException("Пароль должен содержать не менее 8 символов.");
            
        if (_users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
        {
            throw new Exception("Пользователь с таким email уже существует.");
        }
            
        var user = new User(_nextUserId, email, password);
        _nextUserId++;
        this.AddUser(user);
            
        Console.WriteLine("Регистрация прошла успешно.");
        return user;
    }
    public User Login(string email, string password)
    {
        var user = _users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        if (user == null)
            throw new Exception("Пользователь не найден.");
            
        if (user.Password != password) 
            throw new Exception("Неверный пароль.");
            
        Console.WriteLine("Аутентификация прошла успешно.");
        return user;
    }
}