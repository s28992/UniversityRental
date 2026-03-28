namespace UniversityRental.Models;

public class Student : User
{
    public Student(string firstName, string lastName, string indexNumber) : base(firstName, lastName)
    {
        IndexNumber = indexNumber;
    }
    
    public string IndexNumber { get; }
    public override UserType Type => UserType.Student;
    public override string GetDetails()
    {
        return $"Nr indeksu: {IndexNumber}";
    }
}