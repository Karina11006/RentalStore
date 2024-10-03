using RentalStore.SharedKernel.Dto;
using RentalStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalStore.Application.Services
{
    public interface IRentalService
    {
        RentalDto GetById(int id);
        IList<RentalDto> GetAll();
        void Insert(Rental rental);
        void Update(int id, UpdateRentalDto dto);
        void Delete(int id);
        IList<RentalDto> GetActiveRentals();
        int Create(CreateRentalDto dto);
        void CompleteRental(int id);
        RentalDto GetByIdWithDetails(int id);
    }
}
