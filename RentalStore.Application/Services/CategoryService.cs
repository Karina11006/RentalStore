using AutoMapper;
using RentalStore.Domain.Interfaces;
using RentalStore.Domain.Exceptions;
using RentalStore.Domain.Models;
using RentalStore.SharedKernel.Dto;

namespace RentalStore.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRentalStoreUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CategoryService(IRentalStoreUnitOfWork unitOfWork, IMapper mapper)
        {
            this._uow = unitOfWork;
            this._mapper = mapper;
        }

        public int Create(CategoryDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Category is null");
            }

            var maxId = _uow.CategoryRepository.GetMaxId();
            var id = maxId + 1;

            var category = _mapper.Map<Category>(dto);
            category.CategoryId = id;

            _uow.CategoryRepository.Insert(category);
            _uow.Commit();

            return id;
        }

        public void Delete(int id)
        {
            var category = _uow.CategoryRepository.Get(id);
            if (category == null)
            {
               throw new NotFoundException("Category not found");
            }

            _uow.CategoryRepository.Delete(category);
            _uow.Commit();
        }

        public List<CategoryDto> GetAll()
        {
            var categories = _uow.CategoryRepository.GetAll();

            List<CategoryDto> result = _mapper.Map<List<CategoryDto>>(categories);
            return result;
        }

        public CategoryDto GetById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Id is less than zero");
            }

            var category = _uow.CategoryRepository.Get(id);
            if (category == null)
            {
                throw new NotFoundException("Category not found");
            }

            var result = _mapper.Map<CategoryDto>(category);
            return result;
        }

        public void Update(CategoryDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("No category data");
            }

            var category = _uow.CategoryRepository.Get(dto.CategoryId);
            if (category == null)
            {
                throw new NotFoundException("Category not found");
            }

            category.CategoryName = dto.CategoryName;
            category.Description = dto.Description;
            category.ImageUrl = dto.ImageUrl;

            _uow.CategoryRepository.Update(category);
            _uow.Commit();
        }

        

        

       
    }
}
