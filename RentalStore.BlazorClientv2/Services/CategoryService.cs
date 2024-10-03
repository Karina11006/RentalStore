using Newtonsoft.Json;
using RentalStore.SharedKernel.Dto;

namespace RentalStore.BlazorClientv2.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAll();
    }

    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<IEnumerable<CategoryDto>> GetAll()
        {
            var response = await _httpClient.GetAsync("/category");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<IEnumerable<CategoryDto>>(content);
                return categories;
            }
            return new List<CategoryDto>();
        }
    }
}
