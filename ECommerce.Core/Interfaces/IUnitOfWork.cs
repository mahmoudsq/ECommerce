
using ECommerce.Core.Services;

namespace ECommerce.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; }
        public IPhotoRepository PhotoRepository { get; }
        public IProductRepository ProductRepository { get;}
        //public IImageManagementService ImageManagementService { get; }

    }
}
