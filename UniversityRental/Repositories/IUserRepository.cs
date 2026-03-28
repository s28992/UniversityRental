using UniversityRental.Models;

namespace UniversityRental.Repositories;

public interface IUserRepository
{
    void Add(User user);
    User? GetById(Guid id);
    List<User> GetAll();
}