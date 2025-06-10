using ECommerce.Core.Entities;

namespace ECommerce.Core.DTO.DTOProduct
{
    public record PhotoDTO
    {
        public string ImageName { get; set; } = string.Empty;

        public static PhotoDTO ToDTO(Photo photo)
        {
            return new()
            {
                ImageName = photo.ImageName,
            };
        }
    }
}
