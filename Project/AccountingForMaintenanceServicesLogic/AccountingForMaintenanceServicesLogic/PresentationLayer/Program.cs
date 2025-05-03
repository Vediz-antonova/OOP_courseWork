using AccountingForMaintenanceServicesLogic.BusinessLayer;
using AccountingForMaintenanceServicesLogic.BusinessLayer.Interfaces;
using AccountingForMaintenanceServicesLogic.DataLayer;

class Program
{
    static void Main(string[] args)
    {
        IUserService userService = new UserService();
        ICarService carService = new CarService();

        MaintenanceService maintenanceService = new MaintenanceService();
        maintenanceService.MaintenanceAdded += (sender, e) =>
        {
            var record = e.Maintenance;
            try
            {
                var car = carService.GetCarById(record.CarId);
                if (record.Mileage > car.Mileage)
                {
                    carService.UpdateMileage(car.Id, record.Mileage);
                    Console.WriteLine($"Пробег автомобиля обновлен до {record.Mileage} км через событие обслуживания.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при обновлении пробега: " + ex.Message);
            }
        };

        IReminderService reminderService = new ReminderService();
        IServiceStationService stationService = new ServiceStationService();

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("==== Главное Меню ====");
            Console.WriteLine("User Functions:");
            Console.WriteLine(" 1. Регистрация пользователя");
            Console.WriteLine(" 2. Вход пользователя");
            Console.WriteLine(" 3. Обновление данных пользователя");
            Console.WriteLine(" 4. Удаление пользователя");
            Console.WriteLine(" 5. Вывести всех пользователей");

            Console.WriteLine("Car Functions:");
            Console.WriteLine(" 6. Добавить автомобиль");
            Console.WriteLine(" 7. Обновить данные автомобиля");
            Console.WriteLine(" 8. Удалить автомобиль");
            Console.WriteLine(" 9. Вывести автомобили определенного пользователя");
            Console.WriteLine("10. Вывести все автомобили");

            Console.WriteLine("Maintenance Functions:");
            Console.WriteLine("11. Добавить запись обслуживания");
            Console.WriteLine("12. Обновить запись обслуживания");
            Console.WriteLine("13. Удалить запись обслуживания");
            Console.WriteLine("14. Вывести записи обслуживания для автомобиля");
            Console.WriteLine("15. Вывести все записи обслуживания");

            Console.WriteLine("Reminder Functions:");
            Console.WriteLine("16. Создать напоминание");
            Console.WriteLine("17. Обновить напоминание");
            Console.WriteLine("18. Удалить напоминание");
            Console.WriteLine("19. Вывести напоминания для пользователя");
            Console.WriteLine("20. Вывести напоминания для автомобиля");
            Console.WriteLine("21. Вывести все напоминания");

            Console.WriteLine("Service Station Functions:");
            Console.WriteLine("22. Добавить СТО");
            Console.WriteLine("23. Обновить данные СТО");
            Console.WriteLine("24. Удалить СТО");
            Console.WriteLine("25. Вывести все СТО");
            Console.WriteLine("26. Получить СТО по ID");
            Console.WriteLine("27. Поиск ближайших СТО");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите опцию: ");
            string option = Console.ReadLine();
            Console.WriteLine();

            try
            {
                switch (option)
                {
                    case "1":
                        Console.Write("Введите Email: ");
                        string email = Console.ReadLine();
                        Console.Write("Введите пароль (не менее 8 символов): ");
                        string password = Console.ReadLine();
                        var user = userService.Register(email, password);
                        Console.WriteLine($"Пользователь зарегистрирован: ID: {user.Id}, Email: {user.Email}");
                        break;

                    case "2":
                        Console.Write("Введите Email: ");
                        email = Console.ReadLine();
                        Console.Write("Введите пароль: ");
                        password = Console.ReadLine();
                        var loggedUser = userService.Login(email, password);
                        Console.WriteLine($"Добро пожаловать, {loggedUser.Email}!");
                        break;

                    case "3":
                        Console.Write("Введите ID пользователя для обновления: ");
                        int updateUserId = int.Parse(Console.ReadLine());
                        Console.Write("Введите новый Email: ");
                        string newEmail = Console.ReadLine();
                        Console.Write("Введите новый пароль: ");
                        string newPassword = Console.ReadLine();
                        userService.UpdateUser(updateUserId, newEmail, newPassword);
                        Console.WriteLine("Данные пользователя обновлены.");
                        break;

                    case "4":
                        Console.Write("Введите ID пользователя для удаления: ");
                        int deleteUserId = int.Parse(Console.ReadLine());
                        userService.DeleteUser(deleteUserId);
                        Console.WriteLine("Пользователь удален.");
                        break;

                    case "5":
                        var allUsers = userService.GetAllUsers();
                        Console.WriteLine("Список всех пользователей:");
                        foreach (var u in allUsers)
                            Console.WriteLine($"ID: {u.Id}, Email: {u.Email}");
                        break;

                    case "6":
                        Console.Write("Введите марку автомобиля: ");
                        string brand = Console.ReadLine();
                        Console.Write("Введите модель: ");
                        string model = Console.ReadLine();
                        Console.Write("Введите год выпуска: ");
                        int year = int.Parse(Console.ReadLine());
                        Console.Write("Введите пробег: ");
                        int mileage = int.Parse(Console.ReadLine());
                        Console.Write("Введите ID пользователя для этого автомобиля: ");
                        int carUserId = int.Parse(Console.ReadLine());
                        var car = new Car(0, carUserId, brand, model, year, mileage);
                        car = carService.AddCar(car);
                        Console.WriteLine($"Автомобиль добавлен: ID: {car.Id}, {car.Brand} {car.Model}");
                        break;

                    case "7":
                        Console.Write("Введите ID автомобиля для обновления: ");
                        int updateCarId = int.Parse(Console.ReadLine());
                        Console.Write("Введите новую марку: ");
                        string newBrand = Console.ReadLine();
                        Console.Write("Введите новую модель: ");
                        string newModel = Console.ReadLine();
                        Console.Write("Введите новый год выпуска: ");
                        int newYear = int.Parse(Console.ReadLine());
                        Console.Write("Введите новый пробег: ");
                        int newMileage = int.Parse(Console.ReadLine());
                        var existingCar = carService.GetCarById(updateCarId);
                        var updatedCar = new Car(0, existingCar.UserId, newBrand, newModel, newYear, newMileage);
                        carService.UpdateCar(updateCarId, updatedCar);
                        Console.WriteLine("Данные автомобиля обновлены.");
                        break;

                    case "8":
                        Console.Write("Введите ID автомобиля для удаления: ");
                        int deleteCarId = int.Parse(Console.ReadLine());
                        carService.DeleteCar(deleteCarId);
                        Console.WriteLine("Автомобиль удален.");
                        break;

                    case "9":
                        Console.Write("Введите ID пользователя для вывода его автомобилей: ");
                        int listUserId = int.Parse(Console.ReadLine());
                        var userCars = carService.GetCarsByUserId(listUserId);
                        Console.WriteLine($"Список автомобилей для пользователя {listUserId}:");
                        foreach (var c in userCars)
                            Console.WriteLine($"ID: {c.Id}, {c.Brand} {c.Model}, Пробег: {c.Mileage}");
                        break;

                    case "10":
                        var allCars = carService.GetAllCars();
                        Console.WriteLine("Все автомобили:");
                        foreach (var c in allCars)
                            Console.WriteLine($"ID: {c.Id}, {c.Brand} {c.Model}, Пробег: {c.Mileage}");
                        break;

                    case "11":
                        Console.Write("Введите ID автомобиля для обслуживания: ");
                        int maintCarId = int.Parse(Console.ReadLine());
                        Console.Write("Введите дату обслуживания (yyyy-MM-dd): ");
                        DateTime maintDate = DateTime.Parse(Console.ReadLine());
                        Console.Write("Введите пробег при обслуживании: ");
                        int maintMileage = int.Parse(Console.ReadLine());
                        Console.Write("Введите категорию обслуживания (например, 'Замена масла'): ");
                        string maintCategory = Console.ReadLine();
                        Console.Write("Введите артикул запчастей (если есть): ");
                        string partNumber = Console.ReadLine();
                        Console.Write("Введите стоимость обслуживания: ");
                        decimal maintCost = decimal.Parse(Console.ReadLine());
                        Console.Write("Введите дополнительные заметки: ");
                        string maintNote = Console.ReadLine();
                        var maintenance = new Maintenance(0, maintCarId, maintDate, maintMileage, maintCategory,
                            partNumber, maintCost, maintNote);
                        maintenance = maintenanceService.AddMaintenance(maintenance);
                        Console.WriteLine($"Запись обслуживания добавлена: ID: {maintenance.Id}");
                        break;

                    case "12":
                        Console.Write("Введите ID записи обслуживания для обновления: ");
                        int updateMaintId = int.Parse(Console.ReadLine());
                        Console.Write("Введите новую дату обслуживания (yyyy-MM-dd): ");
                        DateTime newMaintDate = DateTime.Parse(Console.ReadLine());
                        Console.Write("Введите новый пробег: ");
                        int newMaintMileage = int.Parse(Console.ReadLine());
                        Console.Write("Введите новую категорию: ");
                        string newMaintCategory = Console.ReadLine();
                        Console.Write("Введите новый артикул запчастей: ");
                        string newPartNumber = Console.ReadLine();
                        Console.Write("Введите новую стоимость: ");
                        decimal newMaintCost = decimal.Parse(Console.ReadLine());
                        Console.Write("Введите новые заметки: ");
                        string newMaintNote = Console.ReadLine();
                        var currentMaint = maintenanceService.GetMaintenanceById(updateMaintId);
                        var updatedMaint = new Maintenance(0, currentMaint.CarId, newMaintDate, newMaintMileage,
                            newMaintCategory, newPartNumber, newMaintCost, newMaintNote);
                        maintenanceService.UpdateMaintenance(updateMaintId, updatedMaint);
                        Console.WriteLine("Запись обслуживания обновлена.");
                        break;

                    case "13":
                        Console.Write("Введите ID записи обслуживания для удаления: ");
                        int deleteMaintId = int.Parse(Console.ReadLine());
                        maintenanceService.DeleteMaintenance(deleteMaintId);
                        Console.WriteLine("Запись обслуживания удалена.");
                        break;

                    case "14":
                        Console.Write("Введите ID автомобиля для вывода записей обслуживания: ");
                        int listMaintCarId = int.Parse(Console.ReadLine());
                        var maintRecords = maintenanceService.GetMaintenancesByCarId(listMaintCarId);
                        Console.WriteLine($"Записи обслуживания для автомобиля {listMaintCarId}:");
                        foreach (var m in maintRecords)
                            Console.WriteLine(
                                $"ID: {m.Id}, Категория: {m.Category}, Пробег: {m.Mileage}, Дата: {m.Date.ToShortDateString()}");
                        break;

                    case "15":
                        var allMaintRecords = maintenanceService.GetAllMaintenances();
                        Console.WriteLine("Все записи обслуживания:");
                        foreach (var m in allMaintRecords)
                            Console.WriteLine(
                                $"ID: {m.Id}, Автомобиль: {m.CarId}, Категория: {m.Category}, Пробег: {m.Mileage}");
                        break;

                    case "16":
                        Console.Write("Введите ID пользователя для напоминания: ");
                        int remUserId = int.Parse(Console.ReadLine());
                        Console.Write("Введите ID автомобиля для напоминания: ");
                        int remCarId = int.Parse(Console.ReadLine());
                        Console.Write("Введите тип напоминания (например, 'Замена масла'): ");
                        string remType = Console.ReadLine();
                        Console.Write("Введите дату напоминания (yyyy-MM-dd): ");
                        DateTime remDueDate = DateTime.Parse(Console.ReadLine());
                        Console.Write("Введите частоту (например, 'ежегодно'): ");
                        string remFrequency = Console.ReadLine();
                        var reminder = reminderService.CreateReminder(new Reminder(0, remUserId, remCarId, remType,
                            remDueDate, remFrequency));
                        Console.WriteLine($"Напоминание создано: ID: {reminder.Id}");
                        break;

                    case "17":
                        Console.Write("Введите ID напоминания для обновления: ");
                        int updateRemId = int.Parse(Console.ReadLine());
                        Console.Write("Введите новый тип напоминания: ");
                        string newRemType = Console.ReadLine();
                        Console.Write("Введите новую дату напоминания (yyyy-MM-dd): ");
                        DateTime newRemDueDate = DateTime.Parse(Console.ReadLine());
                        Console.Write("Введите новую частоту напоминания: ");
                        string newRemFrequency = Console.ReadLine();
                        var currentRem = reminderService.GetReminderById(updateRemId);
                        var updatedRem = new Reminder(0, currentRem.UserId, currentRem.CarId, newRemType, newRemDueDate,
                            newRemFrequency);
                        reminderService.UpdateReminder(updateRemId, updatedRem);
                        Console.WriteLine("Напоминание обновлено.");
                        break;

                    case "18":
                        Console.Write("Введите ID напоминания для удаления: ");
                        int deleteRemId = int.Parse(Console.ReadLine());
                        reminderService.DeleteReminder(deleteRemId);
                        Console.WriteLine("Напоминание удалено.");
                        break;

                    case "19":
                        Console.Write("Введите ID пользователя для вывода напоминаний: ");
                        int listRemUserId = int.Parse(Console.ReadLine());
                        var userReminders = reminderService.GetRemindersByUserId(listRemUserId);
                        Console.WriteLine($"Напоминания для пользователя {listRemUserId}:");
                        foreach (var r in userReminders)
                            Console.WriteLine($"ID: {r.Id}, Тип: {r.Type}, Дата: {r.DueDate.ToShortDateString()}");
                        break;

                    case "20":
                        Console.Write("Введите ID автомобиля для вывода напоминаний: ");
                        int listRemCarId = int.Parse(Console.ReadLine());
                        var carReminders = reminderService.GetRemindersByCarId(listRemCarId);
                        Console.WriteLine($"Напоминания для автомобиля {listRemCarId}:");
                        foreach (var r in carReminders)
                            Console.WriteLine($"ID: {r.Id}, Тип: {r.Type}, Дата: {r.DueDate.ToShortDateString()}");
                        break;

                    case "21":
                        var allReminders = reminderService.GetAllReminders();
                        Console.WriteLine("Все напоминания:");
                        foreach (var r in allReminders)
                            Console.WriteLine($"ID: {r.Id}, Тип: {r.Type}, Дата: {r.DueDate.ToShortDateString()}");
                        break;

                    case "22":
                        Console.Write("Введите название СТО: ");
                        string stationName = Console.ReadLine();
                        Console.Write("Введите адрес СТО: ");
                        string stationAddress = Console.ReadLine();
                        Console.Write("Введите широту (например, 53.9): ");
                        decimal stLat = decimal.Parse(Console.ReadLine());
                        Console.Write("Введите долготу (например, 27.57): ");
                        decimal stLon = decimal.Parse(Console.ReadLine());
                        Console.Write("Введите рейтинг: ");
                        decimal stRating = decimal.Parse(Console.ReadLine());
                        Console.Write("Введите контактную информацию: ");
                        string stContact = Console.ReadLine();
                        var station = stationService.AddServiceStation(new ServiceStation(0, stationName,
                            stationAddress, stLat, stLon, stRating, stContact));
                        Console.WriteLine($"СТО добавлено: ID {station.Id}");
                        break;

                    case "23":
                        Console.Write("Введите ID СТО для обновления: ");
                        int updateStationId = int.Parse(Console.ReadLine());
                        Console.Write("Введите новое название СТО: ");
                        string newStationName = Console.ReadLine();
                        Console.Write("Введите новый адрес СТО: ");
                        string newStationAddress = Console.ReadLine();
                        Console.Write("Введите новую широту: ");
                        decimal newStLat = decimal.Parse(Console.ReadLine());
                        Console.Write("Введите новую долготу: ");
                        decimal newStLon = decimal.Parse(Console.ReadLine());
                        Console.Write("Введите новый рейтинг: ");
                        decimal newStRating = decimal.Parse(Console.ReadLine());
                        Console.Write("Введите новую контактную информацию: ");
                        string newStContact = Console.ReadLine();
                        var updatedStation = new ServiceStation(0, newStationName, newStationAddress, newStLat,
                            newStLon, newStRating, newStContact);
                        stationService.UpdateServiceStation(updateStationId, updatedStation);
                        Console.WriteLine("Данные СТО обновлены.");
                        break;

                    case "24":
                        Console.Write("Введите ID СТО для удаления: ");
                        int deleteStationId = int.Parse(Console.ReadLine());
                        stationService.DeleteServiceStation(deleteStationId);
                        Console.WriteLine("СТО удалено.");
                        break;

                    case "25":
                        var allStations = stationService.GetAllServiceStations();
                        Console.WriteLine("Список всех СТО:");
                        foreach (var st in allStations)
                            Console.WriteLine($"ID: {st.Id}, {st.Name}, {st.Address}, Рейтинг: {st.Rating}");
                        break;

                    case "26":
                        Console.Write("Введите ID СТО для получения: ");
                        int getStationId = int.Parse(Console.ReadLine());
                        var foundStation = stationService.GetServiceStationById(getStationId);
                        Console.WriteLine(
                            $"Найдено СТО: ID: {foundStation.Id}, {foundStation.Name}, {foundStation.Address}, Рейтинг: {foundStation.Rating}");
                        break;

                    case "27":
                        Console.Write("Введите вашу широту: ");
                        decimal searchLat = decimal.Parse(Console.ReadLine());
                        Console.Write("Введите вашу долготу: ");
                        decimal searchLon = decimal.Parse(Console.ReadLine());
                        Console.Write("Введите радиус поиска (км): ");
                        decimal searchRadius = decimal.Parse(Console.ReadLine());
                        var nearestStations =
                            stationService.GetNearestServiceStations(searchLat, searchLon, searchRadius);
                        Console.WriteLine($"СТО в радиусе {searchRadius} км:");
                        foreach (var st in nearestStations)
                            Console.WriteLine($"ID: {st.Id}, {st.Name}, {st.Address}, Рейтинг: {st.Rating}");
                        break;

                    case "0":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
            
            Console.WriteLine();
        }
    }
}