using ECommerce.Core.DTO;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{

    public class CategoryController(IUnitOfWork unitOfWork) : BaseController(unitOfWork)
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var category = await unitOfWork.CategoryRepository.GetAll();
            if (category.IsFailure)
                return BadRequest(new { error = category.Error });

            return Ok(category.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var category = await unitOfWork.CategoryRepository.GetById(id);
            if (category.IsFailure)
                return BadRequest(new { error = category.Error });

            return Ok(category.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Save(CategotyDTO categotyDTO)
        {
            Category category = new()
            {
                Name = categotyDTO.Name,
                Description = categotyDTO.Description,
            };
            var result = await unitOfWork.CategoryRepository.Add(category);
            if (result.IsFailure)
                return BadRequest(new { error = result.Error });

            return Ok(result.SuccessMessage);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCategotyDTO categotyDTO)
        {
            Category category = new()
            {
                Id  = categotyDTO.id,
                Name = categotyDTO.Name,
                Description = categotyDTO.Description,
            };
            var result = await unitOfWork.CategoryRepository.Update(category);
            if (result.IsFailure)
                return BadRequest(new { error = result.Error });

            return Ok(result.SuccessMessage);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await unitOfWork.CategoryRepository.Delete(id);
            if (result.IsFailure)
                return BadRequest(new { error = result.Error });

            return Ok(result.SuccessMessage);
        }
    }
}
