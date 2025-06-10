using Microsoft.AspNetCore.Http;

namespace ECommerce.Core.Services
{
    public interface IImageManagementService
    {
        Task<List<string>> AddImage(IFormFileCollection files,string src);
        void DeleteImage(string src);
    }
}
