namespace BlazorEcommerce.Shared
{
    public class ProductSearchResult
    {
        public List<Product> Products { get; set;} = new List<Product>();
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}