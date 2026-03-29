using UniversityRental.Models;
using UniversityRental.Services;

namespace UniversityRental;

public class ConsoleHelper
{
    public static void ShowHeader(string title)
    {
        Console.WriteLine();
        Console.WriteLine(new string('=', 50));
        Console.WriteLine();
        Console.WriteLine(new string('=' ,50));
    }

    public static void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    public static void ShowUsers(List<User> users)
    {
        if (users.Count == 0)
        {
            Console.WriteLine("Brak użytkowników.");
            return;
        }

        foreach (var user in users)
        {
            Console.WriteLine($"{user.FullName} | Typ: {GetUserTypeText(user.Type)} | ID: {user.Id}");
            Console.WriteLine($"    {user.GetDetails()}");
        }
    }

    public static void ShowEquipment(List<Equipment> equipmentList)
    {
        if (equipmentList.Count == 0)
        {
            Console.WriteLine("Brak sprzętu.");
            return;
        }

        foreach (var equipment in equipmentList)
        {
            Console.WriteLine($"{equipment.Name} ({equipment.GetType().Name}) | Producent: {equipment.Manufacturer}");
            Console.WriteLine($"    ID: {equipment.Id}");
            Console.WriteLine($"    Status: {GetEquipmentStatusText(equipment.Status)}");
            Console.WriteLine($"    {equipment.GetDetails()}");

            if (equipment.Status == EquipmentStatus.Unavailable &&
                !string.IsNullOrWhiteSpace(equipment.UnavailableReason))
            {
                Console.WriteLine($"    Powód niedostępności: {equipment.UnavailableReason}");
            }
        }
    }

    public static void ShowRentals(
        List<Rental> rentals,
        Func<Guid, string> userNameResolver,
        Func<Guid, string> equipmentNameResolver)
    {
        if (rentals.Count == 0)
        {
            Console.WriteLine("Brak wypożyczeń do wyświetlenia.");
            return;
        }

        foreach (var rental in rentals)
        {
            Console.WriteLine($"Sprzęt: {equipmentNameResolver(rental.EquipmentId)}");
            Console.WriteLine($"    Użytkownik: {userNameResolver(rental.UserId)}");
            Console.WriteLine($"    Data wypożyczenia: {rental.BorrowedAt:yyyy-MM-dd}");
            Console.WriteLine($"    Liczba dni: {rental.LoanDays}");
            Console.WriteLine($"    Termin zwrotu: {rental.DueDate:yyyy-MM-dd}");
            Console.WriteLine($"    Aktywne: {(rental.IsActive ? "Tak" : "Nie")}");

            if (rental.ReturnedAt != null)
            {
                Console.WriteLine($"    Data zwrotu: {rental.ReturnedAt:yyyy-MM-dd}");
                Console.WriteLine($"    Kara: {rental.PenaltyAmount:F2} zł");
            }
        }
    }

    public static void ShowReport(RentalReport report)
    {
        Console.WriteLine($"Liczba wszystkich sprzętów: {report.TotalEquipment}");
        Console.WriteLine($"Dostępne: {report.AvailableEquipment}");
        Console.WriteLine($"Wypożyczone: {report.LoanedEquipment}");
        Console.WriteLine($"Niedostępne: {report.UnavailableEquipment}");
        Console.WriteLine($"Liczba użytkowników: {report.TotalUsers}");
        Console.WriteLine($"Aktywne wypożyczenia: {report.ActiveRentals}");
        Console.WriteLine($"Przeterminowane wypożyczenia: {report.OverdueRentals}");
        Console.WriteLine($"Suma naliczonych kar: {report.TotalPenaltyAmount:F2} zł");
    }

    private static string GetUserTypeText(UserType type)
        {
            return type == UserType.Student ? "Student" : "Pracownik";
        }

        private static string GetEquipmentStatusText(EquipmentStatus status)
        {
            return status switch
            {
                EquipmentStatus.Available => "Dostępny",
                EquipmentStatus.Loaned => "Wypożyczony",
                EquipmentStatus.Unavailable => "Niedostępny",
                _ => "Nieznany"
            };
        }
    }