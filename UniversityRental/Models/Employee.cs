namespace UniversityRental.Models;

public class Employee : User
{
    public Employee(string firstName, string lastName, string employeeNumber, string department) : base(firstName, lastName)
    {
        EmployeeNumber = employeeNumber;
        Department = department;
    }
    
    public string EmployeeNumber { get; }
    public string Department { get; }

    public override UserType Type => UserType.Employee;

    public override string GetDetails()
    {
        return $"Nr pracownika: {EmployeeNumber}, Dział: {Department}";
    }
}