using Accounting_for_maintenance_services.DataLayer;
namespace Accounting_for_maintenance_services.BusinessLayer.Interfaces;

public interface ICarService
{
    Car AddCar(Car car);
    void UpdateCar(int carId, Car updatedCar);
    void DeleteCar(int carId);
    Car GetCarById(int carId);
    List<Car> GetCarsByUserId(int userId);
    List<Car> GetAllCars();
}