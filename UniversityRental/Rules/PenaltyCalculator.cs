using System.Runtime.InteropServices.JavaScript;
using UniversityRental.Models;

namespace UniversityRental.Rules;

public class PenaltyCalculator : IPenaltyCalculator
{
    private readonly double _dailyPenalty;

    public PenaltyCalculator(double dailyPenalty)
    {
        _dailyPenalty = dailyPenalty;
    }

    public double CalculatePenalty(Rental rental, DateTime returnDate)
    {
        var lateDays = (returnDate.Date - rental.DueDate.Date).Days;

        if (lateDays <= 0)
        {
            return 0;
        }

        return lateDays * _dailyPenalty;
    }
}