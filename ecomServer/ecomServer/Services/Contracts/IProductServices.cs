using ecomServer.DTO.ProductDTO;
namespace ecomServer.Services.Contracts
{
    public interface IProductServices
    {
        public Task<ProductDTO> InsertProduct(ProductDTO product);
        public Task<ProductDTO> UpdateProduct(ProductDTO product);
        public Task<IEnumerable<ProductDTO>> GetAllProducts();
        public Task<ProductDTO> GetProductById(int productId);
        public Task<bool> DeleteProduct(int productId);

    }
}
