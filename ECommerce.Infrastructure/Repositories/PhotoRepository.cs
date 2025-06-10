using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using ECommerce.Infrastructure.Data;

namespace ECommerce.Infrastructure.Repositories
{
    public class PhotoRepository(AppDbContext context) : 
        GenericRepository<Photo>(context), IPhotoRepository
    {
    }
}
