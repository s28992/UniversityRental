namespace UniversityRental.Models;

public class Projector : Equipment
{
    public Projector(string name, string manufacturer, int lumens, string resolution) : base(name, manufacturer)
    {
        Lumens = lumens;
        Resolution = resolution;
    }
    
    public int Lumens { get; }
    public string Resolution { get; }

    public override string GetDetails()
    {
        return $"Jasność: {Lumens} lm, Rozdzielczość: {Resolution}";
    }
}