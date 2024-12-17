using WebApi.src.Domain.Contract;
using static WebApi.src.Domain.Contract.ProductsContracts;

namespace WebApi.src.Service
{
    public interface IProductService
    {
        Task<ApiResponse<ProductResponse>> GetProduct(Guid Id);
        Task<ApiResponse<IEnumerable<ProductResponse>>> GetProducts();
        Task<ApiResponse<ProductResponse>> CreateProduct(CreateProductRequest request);
        Task<ApiResponse<ProductResponse>> UpdateProduct(Guid id, UpdateProductRequest request);
        Task<ApiResponse<bool>> DeleteProduct(Guid Id);
    }
}
