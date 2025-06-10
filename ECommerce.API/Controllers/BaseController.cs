using ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController(IUnitOfWork unitOfWork) : ControllerBase
    {
        protected IUnitOfWork _unitOfWork = unitOfWork;
    }
}
