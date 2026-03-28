using UniversityRental.Models;

namespace UniversityRental.Rules;

public interface IPenaltyCalculator
{
    double CalculatePenalty(Rental rental, DateTime returnDate);
}