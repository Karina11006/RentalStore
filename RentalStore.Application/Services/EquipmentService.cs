using AutoMapper;
using RentalStore.SharedKernel.Dto;
using RentalStore.Domain.Exceptions;
using RentalStore.Domain.Interfaces;
using RentalStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RentalStore.Application.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IRentalStoreUnitOfWork _uow;
        private readonly IMapper _mapper;

        public EquipmentService(IRentalStoreUnitOfWork unitOfWork, IMapper mapper)
        {
            this._uow = unitOfWork;
            this._mapper = mapper;
        }

        public int Create(EquipmentDto dto)
        {
            var category = _uow.CategoryRepository.Find(c => c.CategoryName == dto.CategoryName).FirstOrDefault();

            if (category == null)
            {
                throw new Exception("Category not found");
            }

            dto.CategoryId = category.CategoryId;

            var equipment = _mapper.Map<Equipment>(dto);

            equipment.ImageUrl = !string.IsNullOrEmpty(category.ImageUrl)
                ? category.ImageUrl
                : "/images/no-image-icon.png";

            _uow.EquipmentRepository.Insert(equipment);
            _uow.Commit();

            return equipment.EquipmentId;
        }


        public void Delete(int id)
        {
            var equipment = _uow.EquipmentRepository.Get(id);
            if (equipment == null)
            {
                throw new NotFoundException("Equipment not found");
            }

            _uow.EquipmentRepository.Delete(equipment);
            _uow.Commit();
        }

        public List<EquipmentDto> GetAll()
        {
            var equipment = _uow.EquipmentRepository.GetAll();
            var categories = _uow.CategoryRepository.GetAll(); // Pobranie wszystkich kategorii

            var result = _mapper.Map<List<EquipmentDto>>(equipment, opt =>
            {
                opt.Items["categories"] = categories; // Przekazanie kategorii do opcji mapowania
            });

            return result;
        }

        public EquipmentDto GetById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Id is less than zero");
            }

            var equipment = _uow.EquipmentRepository.Get(id);
            if (equipment == null)
            {
                throw new NotFoundException("Equipment not found");
            }

            var categories = _uow.CategoryRepository.GetAll(); // Pobranie wszystkich kategorii

            var result = _mapper.Map<EquipmentDto>(equipment, opt =>
            {
                opt.Items["categories"] = categories; // Przekazanie kategorii do opcji mapowania
            });

            return result;
        }

        public void Update(int id, EquipmentDto dto)
        {
            var equipment = _uow.EquipmentRepository.Get(id);
            if (equipment == null)
            {
                throw new NotFoundException("Equipment not found");
            }

            var category = _uow.CategoryRepository.GetCategoryByName(dto.CategoryName);
            if (category == null)
            {
                throw new BadRequestException("Invalid category name");
            }

            _mapper.Map(dto, equipment);
            equipment.CategoryId = category.CategoryId;

            equipment.ImageUrl = !string.IsNullOrEmpty(category.ImageUrl)
                ? category.ImageUrl
                : "/images/no-image-icon.png";

            _uow.EquipmentRepository.Update(equipment);
            _uow.Commit();
        }

        public List<EquipmentDto> GetEquipmentByCategoryName(string categoryName)
        {
            var equipments = _uow.EquipmentRepository.GetEquipmentByCategoryName(categoryName);
            var categories = _uow.CategoryRepository.GetAll(); // Pobranie wszystkich kategorii

            var result = _mapper.Map<List<EquipmentDto>>(equipments, opt =>
            {
                opt.Items["categories"] = categories; // Przekazanie kategorii do opcji mapowania
            });

            return result;
        }
    }
}
