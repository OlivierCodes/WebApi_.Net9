using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.src.Service;
using static WebApi.src.Domain.Contract.ProductsContracts;

namespace WebApi.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        private readonly IValidator<CreateProductRequest> _createProductRequestValidator;
        private readonly IValidator<UpdateProductRequest> _updateProductRequestValidator;

        public ProductController(IProductService productService, IValidator<CreateProductRequest> createProductRequestValidator, IValidator<UpdateProductRequest> updateProductRequestValidator)
        {
            _productService = productService;
            _createProductRequestValidator = createProductRequestValidator;
            _updateProductRequestValidator = updateProductRequestValidator;
        }


        [HttpPost("api/product")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            var validationResult = await _createProductRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid) 
            {
                return BadRequest(validationResult.Errors);
            }
            var response = await _productService.CreateProduct(request);
            return Ok(response);

        }

        [HttpGet("api/products")]
        public async Task<IActionResult> GetProducts()
        {
            var response = await _productService.GetProducts();
            return Ok(response);
        }

        [HttpGet("api/product/{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var response = await _productService.GetProduct(id);
            return Ok(response);
        }

        [HttpPut("api/product/{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductRequest request)
        {
            var validationResult = await _updateProductRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var response = await _productService.UpdateProduct(id, request);
            return Ok(response);
        }

        [HttpDelete("api/product/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var response = await _productService.DeleteProduct(id);
            return Ok(response);
        }



    }
}
