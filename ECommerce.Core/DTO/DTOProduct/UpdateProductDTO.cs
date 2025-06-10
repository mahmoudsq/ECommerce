using ECommerce.Core.Entities;

namespace ECommerce.Core.DTO.DTOProduct
{
    public record UpdateProductDTO : AddProductDTO
    {
        public int Id { get; set; }
    }

    public static class UpdateProductMapper
    {
        public static Product ToUpdateMap(this UpdateProductDTO productDTO)
        {
            return new()
            {
                Id = productDTO.Id,
                Name = productDTO.Name,
                Description = productDTO.Description,
                NewPrice = productDTO.NewPrice,
                CategoryId = productDTO.CategoryId,
            };
        }
    }
}
