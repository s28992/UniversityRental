using UniversityRental.Models;
using UniversityRental.Repositories;
using UniversityRental.Rules;

namespace UniversityRental.Services;

public class RentalService
{
    private readonly IUserRepository _userRepository;
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly IRentalRepository _rentalRepository;
    private readonly IUserLimitRule _userLimitRule;
    private readonly IPenaltyCalculator _penaltyCalculator;

    public RentalService(
        IUserRepository userRepository,
        IEquipmentRepository equipmentRepository,
        IRentalRepository rentalRepository,
        IUserLimitRule userLimitRule,
        IPenaltyCalculator penaltyCalculator)
    {
        _userRepository = userRepository;
        _equipmentRepository = equipmentRepository;
        _rentalRepository = rentalRepository;
        _userLimitRule = userLimitRule;
        _penaltyCalculator = penaltyCalculator;
    }

    public bool TryBorrowEquipment(
        Guid userId,
        Guid equipmentId,
        DateTime borrowDate,
        int loanDays,
        out string message,
        out Rental? rental)
    {
        rental = null;

        if (loanDays <= 0)
        {
            message = "Liczba dni wypożyczenia musi być większa od zera.";
            return false;
        }

        var user = _userRepository.GetById(userId);
        if (user == null)
        {
            message = "Nie znaleziono użytkownika.";
            return false;
        }

        var equipment = _equipmentRepository.GetById(equipmentId);
        if (equipment == null)
        {
            message = "Nie znaleziono sprzętu.";
            return false;
        }
        
        if (equipment.Status != EquipmentStatus.Available)
        {
            message = "Sprzęt nie jest dostępny do wypożyczenia.";
            return false;
        }

        var activeRentals = _rentalRepository.GetActiveByUserId(userId).Count;
        var limit = _userLimitRule.GetMaxActiveRentals(user);

        if (activeRentals >= limit)
        {
            message = $"Użytkownik przekroczył limit aktywnych wypożyczeń ({limit})";
            return false;
        }

        try
        {
            equipment.MarkAsLoaned();
        }
        catch (Exception ex)
        {
            message = ex.Message;
            return false;
        }

        rental = new Rental(userId, equipmentId, borrowDate, loanDays);
        _rentalRepository.Add(rental);

        message = "Wypożyczenie zostało zapisane.";
        return true;
    }

    public bool TryReturnEquipment(Guid rentalId, DateTime returnDate, out string message, out double penalty)
    {
        penalty = 0;

        var rental = _rentalRepository.GetById(rentalId);
        if (rental == null)
        {
            message = "Nie znaleziono wypożyczenia.";
            return false;
        }

        if (!rental.IsActive)
        {
            message = "To wypożyczenie jest już zamknięte.";
            return false;
        }

        var equipment = _equipmentRepository.GetById(rental.EquipmentId);
        if (equipment == null)
        {
            message = "Nie znaleziono sprzętu powiązanego z wypożyczeniem";
            return false;
        }

        penalty = _penaltyCalculator.CalculatePenalty(rental, returnDate);
        rental.Close(returnDate, penalty);
        equipment.MarkAsAvailable();

        if (penalty > 0)
        {
            message = $"Zwrot po terminie. Naliczona kara: {penalty:F2} zł.";
        }
        else
        {
            message = "Zwrot w terminie. Kara nie została naliczona.";
        }

        return true;
    }

    public List<Rental> GetActiveRentalsForUser(Guid userId)
    {
        return _rentalRepository.GetActiveByUserId(userId);
    }

    public List<Rental> GetOverdueRentals(DateTime checkedDate)
    {
        return _rentalRepository
            .GetActiveRentals()
            .Where(r => r.IsOverdue(checkedDate))
            .ToList();
    }
}