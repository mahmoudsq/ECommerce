using ECommerce.Core.DTO.DTOProduct;
using ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{

    public class ProductController(IUnitOfWork unitOfWork) : BaseController(unitOfWork)
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var product = await unitOfWork.ProductRepository
                .GetAll(a => a.Category, a => a.Photos);
            if (product.IsFailure)
                return BadRequest(new { error = product.Error });
            var result = product.Value.Select(ProductDTO.ToDTO);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await unitOfWork.ProductRepository.GetById(id, a => a.Category, a => a.Photos);
            if (product.IsFailure)
                return BadRequest(new { error = product.Error });

            var result = ProductDTO.ToDTO(product.Value!);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Save(AddProductDTO productDTO)
        {
            var result = await unitOfWork.ProductRepository.AddAsync(productDTO);
            if (result.IsFailure)
                return BadRequest(new { error = result.Error });
            return Ok(result.SuccessMessage);

        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductDTO productDTO)
        {
            var result = await unitOfWork.ProductRepository.UpdateAsync(productDTO);
            if (result.IsFailure)
                return BadRequest(new { error = result.Error });
            return Ok(result.SuccessMessage);

        }
    }
}
