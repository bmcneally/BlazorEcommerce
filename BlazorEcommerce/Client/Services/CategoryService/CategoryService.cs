using BlazorEcommerce.Shared;

namespace BlazorEcommerce.Client.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<Category> Categories { get; set; } = new List<Category>();

        public async Task GetCategories()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/category");
            if (response != null && response.Data != null)
            {
                Categories = response.Data;
            }
        }
    }
}