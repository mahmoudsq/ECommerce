using ECommerce.Core.Common;
using ECommerce.Core.DTO.DTOProduct;
using ECommerce.Core.Entities;

namespace ECommerce.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Result> AddAsync(AddProductDTO productDto);
        Task<Result> UpdateAsync(UpdateProductDTO productDto);
    }
}
