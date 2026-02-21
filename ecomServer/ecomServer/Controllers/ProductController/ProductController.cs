using ecomServer.DTO.ProductDTO;
using ecomServer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;


namespace ecomServer.Controllers.ProductController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productService;
      
        public ProductController(IProductServices productServices)
        {
            _productService = productServices;
        }

        [HttpPost]
        [Route("InsertProduct")]
        public async Task<ActionResult> InsertProduct([FromForm]  ProductDTO viewModel)
        {
            try
            {
                return await _productService.InsertProduct(viewModel) != null ? Ok("Product Inserted Successfully") : BadRequest("Failed to Insert Product");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<ActionResult> UpdateProduct(ProductDTO product)
        {
            try
            {
                var updatedProduct = await _productService.UpdateProduct(product);
                if (updatedProduct != null)
                {
                    return Ok(updatedProduct);
                }
                else
                {
                    return NotFound("Product not found or update failed.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteProduct/{id}")]
        public async Task<ActionResult> DeleteProduct(int productId)
        {
            try
            {
               bool Results = await _productService.DeleteProduct(productId);
                return Results ? Ok("Product Deleted Successfully") : NotFound("Product not found or deletion failed.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // GET: api/<ProductController>
        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts([FromQuery] int? categoryId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var result =  await _productService.GetAllProducts(categoryId, pageNumber, pageSize);
                return result != null ? Ok(result) : NotFound("No products found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<ProductController>/5
        [HttpGet]
        [Route("GetProduct/{id}")]
        public async Task<ActionResult> GetProduct(int ProductId)
        {
            try
            {
                var product = await _productService.GetProductById(ProductId);
                return product != null ? Ok(product) : NotFound("Product not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
