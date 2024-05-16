using Microsoft.AspNetCore.Http;

namespace mvcproject.Shared
{
    public interface IFileService
    {
        void DeleteFile(string fileName);

        Task<string> SaveFile(IFormFile file, string[] allowrdExtensions);
    }
}
