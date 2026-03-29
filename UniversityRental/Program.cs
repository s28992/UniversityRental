
using System.Text;
using UniversityRental;
using UniversityRental.Models;
using UniversityRental.Repositories;
using UniversityRental.Rules;
using UniversityRental.Services;

Console.OutputEncoding = Encoding.UTF8;

var userRepository = new UserRepository();
var equipmentRepository = new EquipmentRepository();
var rentalRepository = new RentalRepository();

var userService = new UserService(userRepository);
var equipmentService = new EquipmentService(equipmentRepository);
var rentalService = new RentalService(
    userRepository,
    equipmentRepository,
    rentalRepository,
    new UserLimitRule(),
    new PenaltyCalculator(10));
var reportService = new ReportService(
    userRepository,
    equipmentRepository,
    rentalRepository);

var student = new Student("Jan", "Kowalski", "s12345");
var employee = new Employee("Anna", "Nowak", "e100", "Dział IT");

var laptop1 = new Laptop("Dell Latitude", "Dell", "Intl i5", 16);
var projector1 = new Projector("LG Projector 123", "LG", 3600, "XGA");
var camera1 = new Camera("Canon Camera 1000", "Canon", "APS-C", 10);
var laptop2 = new Laptop("Lenovo Thinkpad E14", "Lenovo", "Intel i7", 16);

ConsoleHelper.ShowHeader("1. Dodawanie użytkowników");
userService.AddUser(student);
userService.AddUser(employee);
ConsoleHelper.ShowUsers(userService.GetAllUsers());

ConsoleHelper.ShowHeader("2. Dodawanie sprzętu");
equipmentService.AddEquipment(laptop1);
equipmentService.AddEquipment(projector1);
equipmentService.AddEquipment(camera1);
equipmentService.AddEquipment(laptop2);
ConsoleHelper.ShowEquipment(equipmentService.GetAllEquipment());

ConsoleHelper.ShowHeader("3. Lista sprzętu dostępnego");
ConsoleHelper.ShowEquipment(equipmentService.GetAvailableEquipment());

ConsoleHelper.ShowHeader("4. Poprawne wypożyczenia");
rentalService.TryBorrowEquipment(student.Id, laptop1.Id, new DateTime(2026, 3, 1), 5, out var message1,
    out var rental1);
ConsoleHelper.ShowMessage(message1);

rentalService.TryBorrowEquipment(student.Id, projector1.Id, new DateTime(2026, 3, 1), 7, out var message2,
    out var rental2);
ConsoleHelper.ShowMessage(message2);

rentalService.TryBorrowEquipment(employee.Id, camera1.Id, new DateTime(2026, 3, 2), 3, out var message3,
    out var rental3);
ConsoleHelper.ShowMessage(message3);

ConsoleHelper.ShowHeader("5. Próba przekroczenia limitu studenta");
rentalService.TryBorrowEquipment(student.Id, laptop2.Id, new DateTime(2026, 3, 2), 4, out var message4, out _);
ConsoleHelper.ShowMessage(message4);

ConsoleHelper.ShowHeader("6. Oznaczenie sprzętu jako niedostępnego");
equipmentService.TryMarkAsUnavailable(laptop2.Id, "Sprzęt jest w serwisie", out var message5);
ConsoleHelper.ShowMessage(message5);

ConsoleHelper.ShowHeader("7. Próba wypożyczenia sprzętu niedostępnego");
rentalService.TryBorrowEquipment(employee.Id, laptop2.Id, new DateTime(2026, 3, 2), 2, out var message6, out _);
ConsoleHelper.ShowMessage(message6);

ConsoleHelper.ShowHeader("8. Aktywne wypożyczenia studenta");
ConsoleHelper.ShowRentals(
    rentalService.GetActiveRentalsForUser(student.Id),
    id => userService.GetUserById(id)?.FullName ?? "Nieznany użytkownik",
    id => equipmentService.GetEquipmentById(id)?.Name ?? "Nieznany sprzęt");

ConsoleHelper.ShowHeader("9. Zwrot w terminie");
if (rental1 != null)
{
    rentalService.TryReturnEquipment(rental1.Id, new DateTime(2026, 3, 5), out var returnMessage1, out var penalty1);
    ConsoleHelper.ShowMessage(returnMessage1);
}

ConsoleHelper.ShowHeader("10. Zwrot po terminie");
if (rental2 != null)
{
    rentalService.TryReturnEquipment(rental2.Id, new DateTime(2026, 3, 10), out var returnMessage2, out var penalty2);
    ConsoleHelper.ShowMessage(returnMessage2);
}

var reportDate = new DateTime(2026, 3, 10);

ConsoleHelper.ShowHeader("11. Lista przeterminowanych wypożyczeń");
ConsoleHelper.ShowRentals(
    rentalService.GetOverdueRentals(reportDate),
    id => userService.GetUserById(id)?.FullName ?? "Nieznany użytkownik",
    id => equipmentService.GetEquipmentById(id)?.Name ?? "Nieznany sprzęt");

ConsoleHelper.ShowHeader("12. Cały sprzęt po operacjach");
ConsoleHelper.ShowEquipment(equipmentService.GetAllEquipment());

ConsoleHelper.ShowHeader("13. Raport końcowy");
var report = reportService.GenerateReport(reportDate);
ConsoleHelper.ShowReport(report);