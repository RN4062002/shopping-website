using ecomServer.DTO.ProductDTO;
using ecomServer.Repositories;
using ecomServer.Repositories.Contracts;
using ecomServer.Services.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace ecomServer.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _productRepository;
        public ProductServices(IProductRepository productRepository) 
        {
            _productRepository = productRepository;
        }
        public async Task<ProductDTO> InsertProduct(ProductDTO productDto)
        {
            if (productDto == null)
            {
                throw new ArgumentNullException(nameof(productDto), "Product cannot be null");
            }
            var product = new Models.Product
            {
                ProductName = productDto.ProductName,
                ProductDesc = productDto.ProductDesc,
                ProductPrice = productDto.ProductPrice,
                ProductStock = productDto.ProductStock,
                CategoryId = productDto.CategoryId,
                ProductImages = productDto.ProductImages,
                CreatedAt = productDto.CreatedAt,
                IsActive = productDto.IsActive
            };
              await _productRepository.AddProductAsync(product);
            return productDto;

        }
        public async Task<ProductDTO> UpdateProduct(ProductDTO productDto)
        {
            var product = await _productRepository.GetProductByIdAsync(productDto.ProductId);
            if (product == null)
            {
                throw new ArgumentNullException(nameof(productDto), "Product not found");
            }
            product.ProductName = productDto.ProductName;
            product.ProductDesc = productDto.ProductDesc;
            product.ProductPrice = productDto.ProductPrice;
            product.ProductStock = productDto.ProductStock;
            product.CategoryId = productDto.CategoryId;
            product.CreatedAt = productDto.CreatedAt;
            product.ProductImages = productDto.ProductImages;
            product.IsActive = productDto.IsActive;
            
            await _productRepository.UpdateProductAsync(product);
            return productDto;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            var result = await _productRepository.DeleteProductAsync(productId);
            return result;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {

            var products = await _productRepository.GetAllProductsAsync();

            var result = products.Select(p => new ProductDTO
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                ProductPrice = p.ProductPrice,
                ProductDesc = p.ProductDesc,
                ProductStock = p.ProductStock,
                ProductImages = p.ProductImages
            }).ToList();

            return result;
        }
        public async Task<ProductDTO> GetProductById(int ProductId)
        {
            var product = await _productRepository.GetProductByIdAsync(ProductId);

            return new ProductDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductDesc = product.ProductDesc,
                ProductPrice = product.ProductPrice,
                ProductStock = product.ProductStock,
                CategoryId = product.CategoryId,
                CreatedAt = product.CreatedAt,
                IsActive = product.IsActive,
                ProductImages = product.ProductImages
            };
        }
    }
}
