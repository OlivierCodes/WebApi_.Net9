using Azure.Core;

namespace WebApi.src.Domain.Contract
{
    public class ProductsContracts
    {
       public record CreateProductRequest(string Name, decimal Price, string Description);
       public record UpdateProductRequest(string Name, decimal Price, string Description);
       public record ProductResponse(Guid Id, string Name, decimal Price, string Description,
            DateTime CreatedAt, DateTime UpdatedAt);

    }

    public class ApiResponse<T>
    {
        public T Data { get; set;}
        public bool Success { get; set;}
        public string Message { get; set;}
    }

}
