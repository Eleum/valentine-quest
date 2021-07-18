using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using Valentine.Api.Services;

namespace Valentine.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private const string StorageContainerName = "images";

        public static IServiceCollection RegisterAzureBlobStorage(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = GetConnectionStringImagesStorage(configuration).ConfigureAwait(false).GetAwaiter().GetResult();

            var account = CloudStorageAccount.Parse(connectionString);
            var client = account.CreateCloudBlobClient();
            var blob = client.GetContainerReference(StorageContainerName);

            var createdSuccessfully = blob.CreateIfNotExistsAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            if (createdSuccessfully)
            {
                blob.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            }

            services.AddSingleton(typeof(CloudBlobContainer), blob);
            return services;
        }

        private static async Task<string> GetConnectionStringImagesStorage(IConfiguration configuration)
        {
            var imagesStorageKeyVaultName = configuration["Azure:KeyVault:ImagesStorageKeyVaultName"];
            var azure = new AzureService();

            return await azure.GetKeyVaultSecretAsync(imagesStorageKeyVaultName);
        }
    }
}
