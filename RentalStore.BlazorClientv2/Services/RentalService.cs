using RentalStore.SharedKernel.Dto;
using System.Net.Http.Json;

namespace RentalStore.BlazorClientv2.Services
{
    public interface IRentalClientService
    {
        Task CreateRentalAsync(CreateRentalDto rental);
    }
    public class RentalClientService : IRentalClientService
    {
        private readonly HttpClient _httpClient;

        public RentalClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateRentalAsync(CreateRentalDto rental)
        {
            var response = await _httpClient.PostAsJsonAsync("api/rental", rental);
            response.EnsureSuccessStatusCode();
        }
    }
}
