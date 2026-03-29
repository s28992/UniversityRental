namespace UniversityRental.Services;

public class RentalReport
{
    public RentalReport(
        int totalEquipment,
        int availableEquipment,
        int loanedEquipment,
        int unavailableEquipment,
        int totalUsers,
        int activeRentals,
        int overdueRentals,
        double totalPenaltyAmount)
    {
        TotalEquipment = totalEquipment;
        AvailableEquipment = availableEquipment;
        LoanedEquipment = loanedEquipment;
        UnavailableEquipment = unavailableEquipment;
        TotalUsers = totalUsers;
        ActiveRentals = activeRentals;
        OverdueRentals = overdueRentals;
        TotalPenaltyAmount = totalPenaltyAmount;
    }
    
    public int TotalEquipment { get; }
    public int AvailableEquipment { get; }
    public int LoanedEquipment { get; }
    public int UnavailableEquipment { get; }
    public int TotalUsers { get; }
    public int ActiveRentals { get; }
    public int OverdueRentals { get; }
    public double TotalPenaltyAmount { get; }
}