using Microsoft.AspNetCore.Http;

namespace ECommerce.Core.DTO.DTOProduct
{
    public record AddProductDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal NewPrice { get; set; }
        public int CategoryId { get; set; }
        public IFormFileCollection Photos { get; set; }
    }
}
