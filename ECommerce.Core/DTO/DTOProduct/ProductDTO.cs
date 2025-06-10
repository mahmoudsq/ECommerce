using ECommerce.Core.Entities;

namespace ECommerce.Core.DTO.DTOProduct;

public record ProductDTO
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal NewPrice { get; set; }
    public string CategoryName { get; set; } = string.Empty;

    public List<PhotoDTO> Photos { get; set; } = [];

    public static ProductDTO ToDTO(Product product)
    {
        return new()
        {
            Name = product.Name,
            Description = product.Description,
            NewPrice = product.NewPrice,
            CategoryName = product.Category.Name,
            Photos = product.Photos.Select(PhotoDTO.ToDTO).ToList(),
        };
    }
}
