using AutoMapper;
using RentalStore.SharedKernel.Dto;
using RentalStore.Domain.Exceptions;
using RentalStore.Domain.Interfaces;
using RentalStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalStore.Application.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalStoreUnitOfWork _uow;
        private readonly IMapper _mapper;

        public RentalService(IRentalStoreUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public int Create(CreateRentalDto dto)
        {
            var rental = _mapper.Map<Rental>(dto);

            foreach (var detail in rental.Details)
            {
                var equipment = _uow.EquipmentRepository.Get(detail.EquipmentId);
                if (equipment == null)
                {
                    throw new NotFoundException("Equipment not found");
                }

                equipment.QuantityInStock -= detail.Count;
                _uow.EquipmentRepository.Update(equipment);
            }

            _uow.RentalRepository.Insert(rental);
            _uow.Commit();

            return rental.RentalId;
        }


        public void Delete(int id)
        {
            var rental = _uow.RentalRepository.GetByIdWithDetails(id);
            if (rental == null)
            {
                throw new NotFoundException("Rental not found");
            }

            // Przywrócenie ilości sprzętu na stan magazynowy
            foreach (var detail in rental.Details)
            {
                var equipment = _uow.EquipmentRepository.Get(detail.EquipmentId);
                if (equipment == null)
                {
                    throw new NotFoundException("Equipment not found");
                }

                equipment.QuantityInStock += detail.Count;
                _uow.EquipmentRepository.Update(equipment);
            }

            
            _uow.RentalRepository.Delete(rental);
            _uow.Commit();
        }

        public RentalDto GetById(int id)
        {
            var rental = _uow.RentalRepository.Get(id);
            if (rental == null)
            {
                throw new NotFoundException("Rental not found");
            }

            return _mapper.Map<RentalDto>(rental);
        }

        public IList<RentalDto> GetAll()
        {
            var rentals = _uow.RentalRepository.GetAll();
            return _mapper.Map<IList<RentalDto>>(rentals);
        }

        public IList<RentalDto> GetActiveRentals()
        {
            var rentals = _uow.RentalRepository.GetActiveRentals();
            return _mapper.Map<IList<RentalDto>>(rentals);
        }

        public void Insert(Rental rental)
        {
            _uow.RentalRepository.Insert(rental);
            _uow.Commit();
        }

        public void Update(int id, UpdateRentalDto dto)
        {
            var rental = _uow.RentalRepository.Get(id);
            if (rental == null)
            {
                throw new NotFoundException("Rental not found");
            }

            _mapper.Map(dto, rental);
            _uow.RentalRepository.Update(rental);
            _uow.Commit();
        }
        public void CompleteRental(int id)
        {
            var rental = _uow.RentalRepository.Get(id);
            if (rental == null)
            {
                throw new NotFoundException("Rental not found");
            }

            rental.Status = RentalStatus.Completed;
            _uow.RentalRepository.Update(rental);

            foreach (var detail in rental.Details)
            {
                var equipment = _uow.EquipmentRepository.Get(detail.EquipmentId);
                if (equipment != null)
                {
                    equipment.QuantityInStock += detail.Count;
                    _uow.EquipmentRepository.Update(equipment);
                }
            }

            _uow.Commit();
        }

        public RentalDto GetByIdWithDetails(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Id is less than zero");
            }
            var rental = _uow.RentalRepository.GetByIdWithDetails(id);
            if (rental == null)
            {
                throw new NotFoundException("Rental Details not found");
            }
            var result = _mapper.Map<RentalDto>(rental);
            return result;
        }



    }
}
