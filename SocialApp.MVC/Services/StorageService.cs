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
            var blobContainer = blobClient.GetContainerReference(containerName);
            await blobContainer.CreateIfNotExistsAsync();
            
            await blobContainer.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            CloudBlockBlob cloudBlockBlob = blobContainer.GetBlockBlobReference(filename);

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);

                var fileToUpload = memoryStream.ToArray();

                if (file != null)
                    await cloudBlockBlob.UploadFromByteArrayAsync(fileToUpload, 0, fileToUpload.Length);
            }
        }
    }
}
