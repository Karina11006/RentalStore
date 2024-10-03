using Newtonsoft.Json;
using RentalStore.SharedKernel.Dto;

namespace RentalStore.BlazorClientv2.Services
{
    public interface IEquipmentService
    {
        Task<IEnumerable<EquipmentDto>> GetAll();
        Task<EquipmentDto> GetById(int id);
    }

    public class EquipmentService : IEquipmentService
    {
        private readonly HttpClient _httpClient;

        public EquipmentService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<IEnumerable<EquipmentDto>> GetAll()
        {
            var response = await _httpClient.GetAsync("/equipment");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var equipments = JsonConvert.DeserializeObject<IEnumerable<EquipmentDto>>(content);
                return equipments;
            }
            return new List<EquipmentDto>();
        }

        public async Task<EquipmentDto> GetById(int id)
        {
            var response = await _httpClient.GetAsync($"/equipment/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var equipment = JsonConvert.DeserializeObject<EquipmentDto>(content);
                return equipment;
            }
            return null;
        }

        public async Task<EquipmentDto> GetByCategory(string categoryName)
        {
            var response = await _httpClient.GetAsync($"/equipment/{categoryName}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var equipment = JsonConvert.DeserializeObject<EquipmentDto>(content);
                return equipment;
            }
            return null;
        }
    }
}
