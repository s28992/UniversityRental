using UniversityRental.Models;

namespace UniversityRental.Repositories;

public interface IRentalRepository
{
    void Add(Rental rental);
    Rental? GetById(Guid id);
    List<Rental> GetAll();
    List<Rental> GetActiveByUserId(Guid id);
    List<Rental> GetActiveRentals();
}