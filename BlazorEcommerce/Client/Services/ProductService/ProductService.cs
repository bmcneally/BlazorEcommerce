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

        public async Task<ServiceResponse<Product>> GetProduct(int productId)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{productId}");
            return response;
        }

        public async Task GetProducts()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product");
            if (response != null && response.Data != null)
            {
                Products = response.Data;
            }
        }
    }
}