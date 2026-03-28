using UniversityRental.Models;
using UniversityRental.Repositories;

namespace UniversityRental.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void AddUser(User user)
    {
        _userRepository.Add(user);
    }

    public User? GetUserById(Guid id)
    {
        return _userRepository.GetById(id);
    }

    public List<User> GetAllUsers()
    {
        return _userRepository.GetAll();
    }
}