using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Valentine.Api.Interfaces;

namespace Valentine.Api.Services
{
    public class FileCloudUploader : IFileCloudUploader
    {
        private readonly IConfiguration _configuration;

        public FileCloudUploader(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> Upload(IFormFile file)
        {
            var container = new BlobContainerClient(
                _configuration["valentine-images-connectionstring"], 
                _configuration["Settings:Images:ImagesBlobName"]);

            var createResponse = await container.CreateIfNotExistsAsync();
            if ((createResponse?.GetRawResponse().Status ?? -1) == 201)
            {
                await container.SetAccessPolicyAsync(PublicAccessType.Blob);
            }

            var blob = container.GetBlobClient(file.FileName);
            await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
            await blob.UploadAsync(file.OpenReadStream(), new BlobHttpHeaders { ContentType = file.ContentType });

            return blob.Uri.ToString();
        }
    }
}
