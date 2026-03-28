using UniversityRental.Models;

namespace UniversityRental.Rules;

public interface IUserLimitRule
{
    int GetMaxActiveRentals(User user);
}