using Accounting_for_maintenance_services.BusinessLayer;
using Accounting_for_maintenance_services.BusinessLayer.Interfaces;
namespace AccountingForMaintenance.Tests;

public class UserServiceTests
{
    private readonly IUserService _userService;

    public UserServiceTests()
    {
        _userService = new UserService();
    }

    [Fact]
    public void Register_ValidInput_ReturnsUser()
    {
        var user = _userService.Register("test@example.com", "password123");
        Assert.NotNull(user);
        Assert.Equal("test@example.com", user.Email);
    }

    [Fact]
    public void Register_DuplicateEmail_ThrowsException()
    {
        _userService.Register("test@example.com", "password123");
        Assert.Throws<Exception>(() => _userService.Register("test@example.com", "newpassword"));
    }

    [Fact]
    public void Login_ValidCredentials_ReturnsUser()
    {
        _userService.Register("test@example.com", "password123");
        var user = _userService.Login("test@example.com", "password123");
        Assert.NotNull(user);
        Assert.Equal("test@example.com", user.Email);
    }

    [Fact]
    public void Login_InvalidPassword_ThrowsException()
    {
        _userService.Register("test@example.com", "password123");
        Assert.Throws<Exception>(() => _userService.Login("test@example.com", "wrongpassword"));
    }

    [Fact]
    public void DeleteUser_RemovesUserSuccessfully()
    {
        var user = _userService.Register("delete@example.com", "password123");
 
        _userService.DeleteUser(user.Id);
 
        Assert.Throws<Exception>(() => _userService.GetUserByEmail("delete@example.com"));
    }
}