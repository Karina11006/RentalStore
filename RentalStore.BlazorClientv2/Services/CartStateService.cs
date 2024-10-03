namespace RentalStore.BlazorClientv2.Services
{
    public class CartStateService
    {
        public decimal TotalAmount { get; set; }
        public int RentalDays { get; set; } = 1; // domyslnie 1 dzien
    }
}
