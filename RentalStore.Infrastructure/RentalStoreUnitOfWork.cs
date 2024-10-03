using RentalStore.Domain.Interfaces;

namespace RentalStore.Infrastructure
{
    public class RentalStoreUnitOfWork : IRentalStoreUnitOfWork
    {
        private readonly RentalStoreDbContext _context;

        
        public IEquipmentRepository EquipmentRepository { get; }
        public IRentalRepository RentalRepository { get; }
        public ICategoryRepository CategoryRepository { get; }


        public RentalStoreUnitOfWork(RentalStoreDbContext context, IEquipmentRepository equipmentRepository,IRentalRepository rentalRepository, ICategoryRepository categoryRepository)
        {
            this._context = context;
            this.EquipmentRepository = equipmentRepository;
            this.RentalRepository = rentalRepository;
            this.CategoryRepository = categoryRepository;
        }


        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
