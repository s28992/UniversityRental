using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace UniversityRental.Models;

public class Rental
{
    public Rental(Guid userId, Guid equipmentId, DateTime borrowedAt, int loanDays)
    {
        if (loanDays <= 0)
        {
            throw new ArgumentException("Liczba dni wypożyczenia musi być większa od zera.");
        }

        Id = Guid.NewGuid();
        UserId = userId;
        EquipmentId = equipmentId;
        BorrowedAt = borrowedAt;
        LoanDays = loanDays;
        DueDate = borrowedAt.AddDays(loanDays);
    }
    
    public Guid Id { get; }
    public Guid UserId { get; }
    public Guid EquipmentId { get; }
    public DateTime BorrowedAt { get; }
    public int LoanDays { get; }
    public DateTime DueDate { get; }
    public DateTime? ReturnedAt { get; private set; }
    public double PenaltyAmount { get; private set; }

    public bool IsActive => ReturnedAt == null;

    public bool IsOverdue(DateTime checkedDate)
    {
        return IsActive && checkedDate.Date > DueDate.Date;
    }

    public void Close(DateTime returnDate, double penaltyAmount)
    {
        if (!IsActive)
        {
            throw new InvalidOperationException("To wypożyczenie jest już zamknięte.").
        }

        ReturnedAt = returnDate;
        PenaltyAmount = penaltyAmount;
    }
}