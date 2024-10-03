namespace RentalStore.Domain.Interfaces
{
    public interface IRentalStoreUnitOfWork : IDisposable
    {
        IEquipmentRepository EquipmentRepository { get; }
        IRentalRepository RentalRepository { get; }
        ICategoryRepository CategoryRepository { get; }

        void Commit();
    }
}
