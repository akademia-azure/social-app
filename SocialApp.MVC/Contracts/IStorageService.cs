using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SocialApp.MVC.Contracts
{
    public interface IStorageService
    {
        Task UploadFile(string containerName, string filename, IFormFile file);
    }
}
