using UniversityRental.Models;

namespace UniversityRental.Repositories;

public class EquipmentRepository : IEquipmentRepository
{
    private readonly List<Equipment> _equipmentList = new List<Equipment>();

    public void Add(Equipment equipment)
    {
        _equipmentList.Add(equipment);
    }

    public Equipment? GetById(Guid id)
    {
        return _equipmentList.FirstOrDefault(e => e.Id == id);
    }

    public List<Equipment> GetAll()
    {
        return _equipmentList.ToList();
    }
}