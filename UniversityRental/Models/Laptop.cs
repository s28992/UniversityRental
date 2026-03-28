namespace UniversityRental.Models;

public class Laptop : Equipment
{
    public Laptop(string name, string manufacturer, string processor, int ramGb) : base(name, manufacturer)
    {
        Processor = processor;
        RamGb = ramGb;
    }
    
    public string Processor { get; }
    public int RamGb { get; }

    public override string GetDetails()
    {
        return $"Processor: {Processor}, RAM: {RamGb} GB";
    }
}