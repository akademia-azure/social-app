using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using SocialApp.MVC.Contracts;

namespace SocialApp.MVC.Services
{
    public class StorageService : IStorageService
    {
        private readonly CloudStorageAccount storageAccount;
        private readonly CloudBlobClient blobClient;

        public StorageService(IConfiguration configuration)
        {
            storageAccount = CloudStorageAccount.Parse(configuration.GetValue<string>("ConnectionStrings:StorageConnection"));
            blobClient = storageAccount.CreateCloudBlobClient();
        }

        public async Task UploadFile(string containerName, string filename, IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
