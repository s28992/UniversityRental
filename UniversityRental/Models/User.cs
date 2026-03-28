namespace UniversityRental.Models;

public abstract class User
{
    protected User(string firstName, string lastName)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
    }
    
    public Guid Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string FullName => $"{FirstName} {LastName}";
    public abstract UserType Type { get; }

    public abstract string GetDetails();
}