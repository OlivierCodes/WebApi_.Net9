using AutoMapper;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WebApi.src.Domain.Contract;
using WebApi.src.Domain.Entities;
using WebApi.src.Infrastrure.Context;
using static WebApi.src.Domain.Contract.ProductsContracts;

namespace WebApi.src.Service
{
    public class ProductServie : IProductService
    {
        private readonly ProductDbContext _context;
        private readonly IMapper _mapper;
        public ProductServie(ProductDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

      
        public async Task<ApiResponse<ProductResponse>> CreateProduct(CreateProductRequest request)
        {
            var product = _mapper.Map<Product>(request);
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return new ApiResponse<ProductResponse>
            {
                Data = _mapper.Map<ProductResponse>(product),
                Success = true,
                Message = "Product created succesfully"
            };
        }

       
        public async Task<ApiResponse<IEnumerable<ProductResponse>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            if(products == null || products.Count == 0)
            {
                return new ApiResponse<IEnumerable<ProductResponse>>
                {
                    Data = new List<ProductResponse>(),
                    Success = true,
                    Message = "No product found"
                };
            }
            return new ApiResponse<IEnumerable<ProductResponse>>
            {
                Data = _mapper.Map<IEnumerable<ProductResponse>>(products),
                Success = true,
                Message = "Products fetched succesfully"
            };
        }


        public async Task<ApiResponse<ProductResponse>> GetProduct(Guid Id)
        {
            var product = _context.Products.FirstOrDefaultAsync(p => p.Id == Id);
            if(product == null)
            {
                return new ApiResponse<ProductResponse>
                {
                    Data = null,
                    Success = false,
                    Message = "Product not found"
                };
            }
            return new ApiResponse<ProductResponse>
            {
                Data = _mapper.Map<ProductResponse>(product),
                Success = true,
                Message = "Product fetched successfully"
            };
        }

        public async Task<ApiResponse<ProductResponse>> UpdateProduct(Guid id,UpdateProductRequest request)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if(product == null)
            {
                return new ApiResponse<ProductResponse>
                {
                    Data = null,
                    Success = false,
                    Message = "Product not found"
                };
            }
            if (!string.IsNullOrEmpty(request.Name))
            {
                product.Name = request.Name;
            }

            if (!string.IsNullOrEmpty(request.Description))
            {
                product.Description = request.Description;
            }

            if(request.Price != default) 
            {
                product.Price = request.Price;
            }
            product.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return new ApiResponse<ProductResponse>
            {
                Data = _mapper.Map<ProductResponse>(product),
                Success = true,
                Message = "Product updated successfully"
            };

        }

        public async Task<ApiResponse<bool>> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return new ApiResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Product not found"
                };
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync(true);
            return new ApiResponse<bool>
            {
                Data = true,
                Success = true,
                Message = "Product deleted successfully"
            };
        } 
    }
}
