using UniversityRental.Models;
using UniversityRental.Repositories;

namespace UniversityRental.Services;

public class EquipmentService
{
    private readonly IEquipmentRepository _equipmentRepository;

    public EquipmentService(IEquipmentRepository equipmentRepository)
    {
        _equipmentRepository = equipmentRepository;
    }

    public void AddEquipment(Equipment equipment)
    {
        _equipmentRepository.Add(equipment);
    }

    public Equipment? GetEquipmentById(Guid id)
    {
        return _equipmentRepository.GetById(id);
    }

    public List<Equipment> GetAllEquipment()
    {
        return _equipmentRepository.GetAll();
    }

    public List<Equipment> GetAvailableEquipment()
    {
        return _equipmentRepository
            .GetAll()
            .Where(e => e.Status == EquipmentStatus.Available)
            .ToList();
    }

    public bool TryMarkAsUnavailable(Guid equipmentId, string reason, out string message)
    {
        var equipment = _equipmentRepository.GetById(equipmentId);

        if (equipment == null)
        {
            message = "Nie znaleziono sprzętu.";
            return false;
        }

        try
        {
            equipment.MarkAsUnavailable(reason);
            message = "Sprzęt został oznaczony jako niedostępny.";
            return true;
        }
        catch (Exception ex)
        {
            message = ex.Message;
            return false;
        }
    }
}