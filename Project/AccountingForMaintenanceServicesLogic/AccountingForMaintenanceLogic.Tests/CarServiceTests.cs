using AccountingForMaintenanceServicesLogic.BusinessLayer;
using AccountingForMaintenanceServicesLogic.BusinessLayer.Interfaces;
using AccountingForMaintenanceServicesLogic.DataLayer;
namespace AccountingForMaintenanceLogic.Tests;

public class CarServiceTests
{
    private readonly ICarService _carService;

    public CarServiceTests()
    {
        _carService = new CarService();
    }

    [Fact]
    public void AddCar_ValidInput_ReturnsCar()
    {
        var car = new Car(0, 1, "Toyota", "Camry", 2020, 15000);
        var addedCar = _carService.AddCar(car);
        Assert.NotNull(addedCar);
        Assert.Equal("Toyota", addedCar.Brand);
    }

    [Fact]
    public void UpdateMileage_ValidInput_UpdatesMileage()
    {
        var car = _carService.AddCar(new Car(0, 1, "Toyota", "Camry", 2020, 15000));
        _carService.UpdateMileage(car.Id, 20000);
        var updatedCar = _carService.GetCarById(car.Id);
        Assert.Equal(20000, updatedCar.Mileage);
    }

    [Fact]
    public void UpdateMileage_InvalidMileage_DoesNotUpdate()
    {
        var car = _carService.AddCar(new Car(0, 1, "Toyota", "Camry", 2020, 15000));
        _carService.UpdateMileage(car.Id, 10000);
        var updatedCar = _carService.GetCarById(car.Id);
        Assert.Equal(15000, updatedCar.Mileage);
    }
    
    [Fact]
    public void DeleteCar_RemovesCarSuccessfully()
    {
        var car = _carService.AddCar(new Car(0, 1, "Toyota", "Corolla", 2020, 10000));

        _carService.DeleteCar(car.Id);

        Assert.Throws<Exception>(() => _carService.GetCarById(car.Id));
    }
}