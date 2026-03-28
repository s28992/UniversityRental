namespace UniversityRental.Models;

public abstract class Equipment
{
    protected Equipment(string name, string manufacturer)
    {
        Id = Guid.NewGuid();
        Name = name;
        Manufacturer = manufacturer;
        Status = EquipmentStatus.Available;
    }

    public Guid Id { get; }
    public string Name { get; }
    public string Manufacturer { get; }
    public EquipmentStatus Status { get; private set; }
    public string? UnavailableReason { get; private set; }

    public void MarkAsLoaned()
    {
        if (Status != EquipmentStatus.Available)
        {
            throw new InvalidOperationException("Sprzęt nie jest dostępny do wypożyczenia.");
        }

        Status = EquipmentStatus.Loaned;
        UnavailableReason = null;
    }

    public void MarkAsAvailable()
    {
        Status = EquipmentStatus.Available;
        UnavailableReason = null;
    }

    public void MarkAsUnavailable(string reason)
    {
        if (string.IsNullOrWhiteSpace(reason))
        {
            throw new ArgumentException("Powód niedostępności nie może być pusty.");
        }

        if (Status == EquipmentStatus.Loaned)
        {
            throw new InvalidOperationException("Nie można oznaczyć wypożyczonego sprzętu jako niedostępnego.");
        }

        Status = EquipmentStatus.Unavailable;
        UnavailableReason = reason;
    }

    public abstract string GetDetails();
}