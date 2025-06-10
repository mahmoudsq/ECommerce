using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using ECommerce.Infrastructure.Data;

namespace ECommerce.Infrastructure.Repositories
{
    public class CategoryRepository(AppDbContext context) :
        GenericRepository<Category>(context), ICategoryRepository
    {
    }
}
