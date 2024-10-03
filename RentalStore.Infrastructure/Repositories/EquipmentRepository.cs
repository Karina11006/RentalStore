using Microsoft.EntityFrameworkCore;
using RentalStore.Domain.Interfaces;
using RentalStore.Domain.Models;
using System.Linq.Expressions;

namespace RentalStore.Infrastructure.Repositories
{
    public class EquipmentRepository : Repository<Equipment>, IEquipmentRepository
    {
        private readonly RentalStoreDbContext _rentalStoreDbContext;

        public EquipmentRepository(RentalStoreDbContext context)
            : base(context)
        {
            _rentalStoreDbContext = context;
        }

        public int GetMaxId()
        {
            return _rentalStoreDbContext.Equipments.Max(x => x.EquipmentId);
        }
 
        public IList<Equipment> GetAvailableEquipments()
        {
            return _rentalStoreDbContext.Equipments.Where(e => e.Availability).ToList();
        }

        public IList<Equipment> GetEquipmentByCategoryName(string categoryName)
        {
            return _rentalStoreDbContext.Equipments
                .Where(e => e.Category.CategoryName == categoryName)
                .ToList();
        }


        public Equipment GetEquipmentByName(string name)
        {
            return _rentalStoreDbContext.Equipments.FirstOrDefault(e => e.Name == name);
        }

    }
}
