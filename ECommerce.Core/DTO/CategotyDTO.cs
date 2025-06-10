
namespace ECommerce.Core.DTO
{
    public record CategotyDTO(string Name, string Description);
    public record UpdateCategotyDTO(int id, string Name, string Description);
}
