
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ecomServer.DTO.ProductDTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int StockQuantity { get; set; }
        public List<IFormFile> Images { get; set; }
        public List<string> ImageUrls { get; set; } // Added for retrieving image URLs
        public List<string> ImageUrlsToDelete { get; set; } // Added for deleting images
        public DateTime? CreatedAt { get; set; }
        public bool? IsActive { get; set; }
    }
}
