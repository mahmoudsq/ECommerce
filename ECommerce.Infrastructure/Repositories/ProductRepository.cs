using ECommerce.Core.Common;
using ECommerce.Core.DTO.DTOProduct;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using ECommerce.Core.Mapping;
using ECommerce.Core.Services;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories
{
    public class ProductRepository(AppDbContext context, IImageManagementService imageManagementService) :
        GenericRepository<Product>(context), IProductRepository
    {
        private readonly IImageManagementService _imageManagementService = imageManagementService;
        public async Task<Result> AddAsync(AddProductDTO productDto)
        {
            if (productDto == null) Result.Failure("Entity cannot be null", ErrorType.Validation);

            Product product = productDto!.ToMap();
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            List<string> imagePaths = await _imageManagementService.
                AddImage(productDto!.Photos, productDto.Name);

            List<Photo> photos = imagePaths.Select(a => new Photo
            {
                ImageName = a,
                ProductId = product.Id
            }).ToList();
            await context.Photos.AddRangeAsync(photos);
            await context.SaveChangesAsync();
            return Result.Success("Success");
        }

        public async Task<Result> UpdateAsync(UpdateProductDTO productDto)
        {
            if (productDto == null) Result.Failure("Entity cannot be null", ErrorType.Validation);

            Product? product = await context.Products.Include(x => x.Photos).
                FirstOrDefaultAsync(x => x.Id == productDto!.Id);
            if (product is null)
                return Result.Failure("Entity cannot be null", ErrorType.NotFound);
            product.Name = productDto.Name;
            product.Description = productDto.Name;
            product.NewPrice = productDto.NewPrice;
            product.CategoryId = productDto.CategoryId;
            List<Photo> oldPhotos = [.. product.Photos];
            foreach (var item in oldPhotos)
            {
                _imageManagementService.DeleteImage(item.ImageName);
            }
            context.Photos.RemoveRange(oldPhotos);

            List<string> imagePaths = await _imageManagementService.
                AddImage(productDto!.Photos, productDto.Name);

            List<Photo> photos = imagePaths.Select(a => new Photo
            {
                ImageName = a,
                ProductId = product.Id
            }).ToList();
            await context.Photos.AddRangeAsync(photos);

            await context.SaveChangesAsync();
            return Result.Success("Success");

        }
    }
}
