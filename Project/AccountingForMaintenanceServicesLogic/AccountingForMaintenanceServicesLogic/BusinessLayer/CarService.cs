using AccountingForMaintenanceServicesLogic.BusinessLayer.Interfaces;
using AccountingForMaintenanceServicesLogic.DataLayer;

namespace AccountingForMaintenanceServicesLogic.BusinessLayer;

public class CarService : ICarService
{
    private readonly List<Car> _cars = new List<Car>();
    private int _nextCarId = 1;
    
    public Car AddCar(Car car)
    {
        if (string.IsNullOrWhiteSpace(car.Brand))
            throw new ArgumentException("Бренд автомобиля обязателен.");
        if (string.IsNullOrWhiteSpace(car.Model))
            throw new ArgumentException("Модель автомобиля обязателена.");

        car.Id = _nextCarId++;
        _cars.Add(car);

        Console.WriteLine("Автомобиль успешно добавлен.");
        return car;
    }
    public void UpdateCar(int carId, Car updatedCar)
    {
        var car = _cars.FirstOrDefault(c => c.Id == carId);
        if (car == null)
            throw new Exception("Автомобиль не найден.");

        car.Brand = updatedCar.Brand;
        car.Model = updatedCar.Model;
        car.Year = updatedCar.Year;
        car.Mileage = updatedCar.Mileage;

        Console.WriteLine("Информация о автомобиле успешно обновлена.");
    }
    public void DeleteCar(int carId)
    {
        var car = _cars.FirstOrDefault(c => c.Id == carId);
        if (car == null)
            throw new Exception("Автомобиль не найден.");

        _cars.Remove(car);
        Console.WriteLine("Автомобиль успешно удалён.");
    }
    public Car GetCarById(int carId)
    {
        var car = _cars.FirstOrDefault(c => c.Id == carId);
        return car ?? throw new Exception("Автомобиль не найден.");
    }
    public List<Car> GetCarsByUserId(int userId)
    {
        return _cars.Where(c => c.UserId == userId).ToList();
    }
    public List<Car> GetAllCars()
    {
        return _cars;
    }
    
    public void UpdateMileage(int carId, int newMileage)
    {
        var car = GetCarById(carId);
        if (newMileage > car.Mileage)
        {
            car.Mileage = newMileage;
            Console.WriteLine($"Пробег автомобиля (ID: {carId}) обновлён до {newMileage} км.");
        }
        else
        {
            Console.WriteLine("Новый пробег меньше или равен текущему — обновление не выполнено.");
        }
    }
}