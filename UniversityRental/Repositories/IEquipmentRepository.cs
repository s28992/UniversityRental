using UniversityRental.Models;

namespace UniversityRental.Repositories;

public interface IEquipmentRepository
{
    void Add(Equipment equipment);
    Equipment? GetById(Guid id);
    List<Equipment> GetAll();
}