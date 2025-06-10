using ECommerce.Core.Interfaces;
using ECommerce.Core.Services;
using ECommerce.Infrastructure.Data;

namespace ECommerce.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IImageManagementService _imageManagementService;
        public ICategoryRepository CategoryRepository { get; }
        public IPhotoRepository PhotoRepository { get; }
        public IProductRepository ProductRepository { get; }

        //public IImageManagementService ImageManagementService { get ; }

        public UnitOfWork(AppDbContext context, IImageManagementService imageManagementService)
        {
            _context = context;
            _imageManagementService = imageManagementService;
            CategoryRepository = new CategoryRepository(_context);
            PhotoRepository = new PhotoRepository(_context);
            ProductRepository = new ProductRepository(_context, _imageManagementService);
            //ImageManagementService = new 
        }
    }
}
