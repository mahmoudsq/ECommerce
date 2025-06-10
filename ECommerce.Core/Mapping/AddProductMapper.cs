using ECommerce.Core.DTO.DTOProduct;
using ECommerce.Core.Entities;

namespace ECommerce.Core.Mapping;

public static class AddProductMapper
{
    public static Product ToMap(this AddProductDTO productDTO)
    {
        return new()
        {
            Name = productDTO.Name,
            Description = productDTO.Description,
            NewPrice = productDTO.NewPrice,
            CategoryId = productDTO.CategoryId,
        };
    }
}
