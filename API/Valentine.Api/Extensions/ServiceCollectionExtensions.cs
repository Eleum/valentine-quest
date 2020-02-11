using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Valentine.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private const string BlobContainerName = "images";

        public static IServiceCollection RegisterAzureBlobStorage(this IServiceCollection services, IConfiguration configuration)
        {
            var storageConnectionString = configuration["Azure:Storage:ConnectionString"];
            var account = CloudStorageAccount.Parse(storageConnectionString);
            var client = account.CreateCloudBlobClient();
            var blob = client.GetContainerReference(BlobContainerName);

            var createdSuccessfully = blob.CreateIfNotExistsAsync().Result;

            if (createdSuccessfully)
            {
                blob.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            }

            services.AddSingleton(typeof(CloudBlobContainer), blob);
            return services;
        }
    }
}
