namespace Accounting_for_maintenance_services.DataLayer;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }  
    public DateTime CreatedAt { get; set; }
    
    public User(int id, string email, string password)
    {
        Id = id;
        Email = email;
        Password = password;
        CreatedAt = DateTime.Now; 
    }
}