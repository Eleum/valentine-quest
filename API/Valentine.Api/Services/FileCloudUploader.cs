//using System.Threading.Tasks;
//using Microsoft.WindowsAzure.Storage.Blob;
//using Valentine.Api.Interfaces;

//namespace Valentine.Api.Services
//{
//    public class FileCloudUploader : IFileCloudUploader
//    {
//        private readonly CloudBlobContainer _blobContainer;

//        public FileCloudUploader(CloudBlobContainer blobContainer)
//        {
//            _blobContainer = blobContainer;
//        }

//        public async Task<string> Upload(byte[] fileBytes, string fileName, string contentType)
//        {
//            if (fileBytes == null || fileBytes.Length == 0)
//                return null;

//            var blob = _blobContainer.GetBlockBlobReference(fileName);
//            blob.Properties.ContentType = contentType;

//            await blob.UploadFromByteArrayAsync(fileBytes, 0, fileBytes.Length);
//            return blob.Uri.ToString();
//        }
//    }
//}
