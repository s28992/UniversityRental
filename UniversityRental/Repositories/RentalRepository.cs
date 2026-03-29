using UniversityRental.Models;

namespace UniversityRental.Repositories;

public class RentalRepository : IRentalRepository
{
    private readonly List<Rental> _rentals = new List<Rental>();

    public void Add(Rental rental)
    {
        _rentals.Add(rental);
    }

    public Rental? GetById(Guid id)
    {
        return _rentals.FirstOrDefault(r => r.Id == id);
    }

    public List<Rental> GetAll()
    {
        return _rentals.ToList();
    }

    public List<Rental> GetActiveByUserId(Guid userId)
    {
        return _rentals
            .Where(r => r.UserId == userId && r.IsActive)
            .ToList();
    }

    public List<Rental> GetActiveRentals()
    {
        return _rentals
            .Where(r => r.IsActive)
            .ToList();
    }
}