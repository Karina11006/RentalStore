using RentalStore.SharedKernel.Dto;

namespace RentalStore.Application.Services
{
    public interface ICategoryService
    {
        List<CategoryDto> GetAll();
        CategoryDto GetById(int id);
        int Create(CategoryDto dto);
        void Update(CategoryDto dto);
        void Delete(int id);
    }
}
