using Microsoft.JSInterop;
using RentalStore.SharedKernel.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalStore.BlazorClientv2.Services
{
    public interface ICartService
    {
        Task AddToCart(EquipmentDto equipment);
        Task<List<CartItemDto>> GetCartItems();
        Task RemoveFromCart(int equipmentId);
        Task UpdateCartItemQuantity(int equipmentId, int quantity);
        Task SaveCartItems(List<CartItemDto> cart);
        Task ClearCart();
    }

    public class CartService : ICartService
    {
        private readonly IJSRuntime _jsRuntime;

        public CartService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task AddToCart(EquipmentDto equipment)
        {
            var cart = await GetCartItems();
            if (cart == null)
            {
                cart = new List<CartItemDto>();
            }

            if (equipment == null)
            {
                throw new ArgumentNullException(nameof(equipment), "Equipment cannot be null");
            }


            var existingItem = cart.FirstOrDefault(e => e.Equipment != null && e.Equipment.EquipmentId == equipment.EquipmentId);
            if (existingItem == null)
            {
                cart.Add(new CartItemDto { Equipment = equipment, Quantity = 1 });
                await SaveCartItems(cart);
            }
        }

        public async Task<List<CartItemDto>> GetCartItems()
        {
            var cartJson = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "cart");
            return cartJson == null ? new List<CartItemDto>() : System.Text.Json.JsonSerializer.Deserialize<List<CartItemDto>>(cartJson);
        }

        public async Task RemoveFromCart(int equipmentId)
        {
            var cart = await GetCartItems();
            var item = cart.Find(e => e.Equipment.EquipmentId == equipmentId);
            if (item != null)
            {
                cart.Remove(item);
                await SaveCartItems(cart);
            }
        }

        public async Task UpdateCartItemQuantity(int equipmentId, int quantity)
        {
            var cart = await GetCartItems();
            var item = cart.Find(e => e.Equipment.EquipmentId == equipmentId);
            if (item != null)
            {
                item.Quantity = quantity;
                await SaveCartItems(cart);
            }
        }

        public async Task SaveCartItems(List<CartItemDto> cart)
        {
            var cartJson = System.Text.Json.JsonSerializer.Serialize(cart);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "cart", cartJson);
        }

        public async Task ClearCart()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "cart");
        }
    }
}
    