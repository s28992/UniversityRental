using System.Runtime.InteropServices.JavaScript;
using UniversityRental.Models;
using UniversityRental.Repositories;

namespace UniversityRental.Services;

public class ReportService
{
    private readonly IUserRepository _userRepository;
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly IRentalRepository _rentalRepository;

    public ReportService(
        IUserRepository userRepository,
        IEquipmentRepository equipmentRepository,
        IRentalRepository rentalRepository)
    {
        _userRepository = userRepository;
        _equipmentRepository = equipmentRepository;
        _rentalRepository = rentalRepository;
    }

    public RentalReport GenerateReport(DateTime checkedDate)
    {
        var allEquipment = _equipmentRepository.GetAll();
        var allUsers = _userRepository.GetAll();
        var allRentals = _rentalRepository.GetAll();

        return new RentalReport(
            totalEquipment: allEquipment.Count,
            availableEquipment: allEquipment.Count(e => e.Status == EquipmentStatus.Available),
            loanedEquipment: allEquipment.Count(e => e.Status == EquipmentStatus.Loaned),
            unavailableEquipment: allEquipment.Count(e => e.Status == EquipmentStatus.Unavailable),
            totalUsers: allUsers.Count,
            activeRentals: allRentals.Count(r => r.IsActive),
            overdueRentals: allRentals.Count(r => r.IsOverdue(checkedDate)),
            totalPenaltyAmount: allRentals.Sum(r => r.PenaltyAmount)
        );
    }
}