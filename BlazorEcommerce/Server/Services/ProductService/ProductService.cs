﻿using BlazorEcommerce.Server.Data;

namespace BlazorEcommerce.Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Product>>> GetProducts()
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products.Include(p => p.Variants).ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<Product>> GetProduct(int productId)
        {
            var response = new ServiceResponse<Product>();

            var product = await _context.Products
                .Include(p => p.Variants)
                    .ThenInclude(v => v.ProductType)
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                response.Success = false;
                response.Message = "Specified product was not found.";
            }
            else
            {
                response.Data = product;
            }

            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl)
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products
                    .Where(x => x.Category.Url.ToLower() == categoryUrl.ToLower())
                    .Include(p => p.Variants)
                    .ToListAsync()
            };

            return response;
        }
    }
}