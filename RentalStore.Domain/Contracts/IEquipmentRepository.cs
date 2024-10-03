using RentalStore.Domain.Models;

namespace RentalStore.Domain.Interfaces
{
    public interface IEquipmentRepository : IRepository<Equipment>
    {
        IList<Equipment> GetAvailableEquipments();
        Equipment GetEquipmentByName(string name);
        IList<Equipment> GetEquipmentByCategoryName(string categoryName);

        int GetMaxId();
        
    }
}
