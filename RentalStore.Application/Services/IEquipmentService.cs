using RentalStore.SharedKernel.Dto;

namespace RentalStore.Application.Services
{
    public interface IEquipmentService
    {
        List<EquipmentDto> GetAll();
        EquipmentDto GetById(int id);
        int Create(EquipmentDto dto);
        void Update(int id, EquipmentDto dto);
        void Delete(int id);
        List<EquipmentDto> GetEquipmentByCategoryName(string categoryName); 
    }
}
