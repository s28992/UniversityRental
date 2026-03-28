using UniversityRental.Models;

namespace UniversityRental.Rules;

public class UserLimitRule : IUserLimitRule
{
    public int GetMaxActiveRentals(User user)
    {
        return user.Type switch
        {
            UserType.Student => 2,
            UserType.Employee => 5,
            _ => throw new InvalidOperationException("Nieobsługiwany typ użytkownika.")
        };
    }
}