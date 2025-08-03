using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using TrandingSystem.Domain.Interfaces;
namespace TrandingSystem.Infrastructure.Services
{
    public class WasabiUploader: IWasabiUploader
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public WasabiUploader(IConfiguration config)
        {
            var accessKey = config["Wasabi:AccessKey"];
            var secretKey = config["Wasabi:SecretKey"];
            _bucketName = config["Wasabi:BucketName"];
            //var region = RegionEndpoint.GetBySystemName(config["Wasabi:Region"]);
            var serviceUrl = config["Wasabi:ServiceURL"];

            var s3Config = new AmazonS3Config
            {

                //RegionEndpoint = region,
                ServiceURL = serviceUrl,
                ForcePathStyle = true 
            };

            _s3Client = new AmazonS3Client(accessKey, secretKey, s3Config);
        }

        // ✅ Upload method with unique file name
        public async Task<string?> UploadFileAsync(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return null;

                var uniqueFileName = Path.GetFileNameWithoutExtension(file.FileName)
                                   + "_" + DateTime.UtcNow.Ticks
                                   + Path.GetExtension(file.FileName);

                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = memoryStream,
                    Key = uniqueFileName,
                    BucketName = _bucketName,
                    ContentType = file.ContentType ?? "application/octet-stream",
                    CannedACL = S3CannedACL.Private
                };

                var transferUtility = new TransferUtility(_s3Client);
                await transferUtility.UploadAsync(uploadRequest);

                return uniqueFileName;
            }
            catch (AmazonS3Exception s3Ex)
            {
                Console.WriteLine("❌ AWS Error: " + s3Ex.Message);
                Console.WriteLine("🔁 Status Code: " + s3Ex.StatusCode);
                Console.WriteLine("🔑 Error Code: " + s3Ex.ErrorCode);
                Console.WriteLine("📩 Request ID: " + s3Ex.RequestId);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ General Error: " + ex.Message);
                return null;
            }
        }


    }
}
