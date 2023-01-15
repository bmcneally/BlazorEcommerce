using BlazorEcommerce.Shared;

namespace BlazorEcommerce.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<Product> Products { get; set; } = new List<Product>();
        public string Message { get; set; } = "Loading products...";
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 0;
        public string LastSearchText { get; set; } = String.Empty;

        public event Action ProductsChanged;

        public async Task GetProducts(string? categoryUrl = null)
        {
            var uri = categoryUrl == null ? "api/product/featured" : $"api/product/category/{categoryUrl}";

            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>(uri);
            if (response != null && response.Data != null)
            {
                Products = response.Data;
            }

            CurrentPage = 1;
            TotalPages = 0;

            if (Products.Count == 0)
            {
                Message = "No products found";
            }

            ProductsChanged?.Invoke();
        }

        public async Task<ServiceResponse<Product>> GetProduct(int productId)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{productId}");
            return response;
        }

        public async Task SearchProducts(string searchText, int page)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<ProductSearchResult>>($"api/product/search/{searchText}/{page}");
            if (response != null && response.Data != null)
            {
                Products = response.Data.Products;
                CurrentPage = response.Data.CurrentPage;
                TotalPages = response.Data.TotalPages;
            }

            if (Products.Count == 0)
            {
                Message = "No matching products found.";
            }

            ProductsChanged?.Invoke();
        }

        public async Task<List<string>> GetProductSearchSuggestions(string searchText)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/product/searchsuggestions/{searchText}");
            return response.Data;
        }
    }
}