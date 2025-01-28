    using Amazon.S3;
    using Amazon.S3.Model;
    using Shared.IServices;

    namespace Shared.Services
    {
        public class S3Service : IS3Service
        {
            private readonly IAmazonS3 _s3Client;

            public S3Service(IAmazonS3 s3Client)
            {
                _s3Client = s3Client;
            }

            public async Task UploadFileAsync(string bucketName, string key, Stream inputStream)
            {
                var putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = key,
                    InputStream = inputStream
                };

                await _s3Client.PutObjectAsync(putRequest);
            }

            public async Task<Stream> GetFileAsync(string bucketName, string key)
            {
                var getRequest = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = key
                };

                var response = await _s3Client.GetObjectAsync(getRequest);
                return response.ResponseStream;
            }

            public async Task DeleteFileAsync(string bucketName, string key)
            {
                var deleteRequest = new DeleteObjectRequest
                {
                    BucketName = bucketName,
                    Key = key
                };

                await _s3Client.DeleteObjectAsync(deleteRequest);
            }

            public async Task<string> GetImageBase64Async(string bucketName, string key)
            {
	            var request = new GetObjectRequest
	            {
		            BucketName = bucketName,
		            Key = key
	            };

	            using (var response = await _s3Client.GetObjectAsync(request))
	            using (var responseStream = response.ResponseStream)
	            using (var memoryStream = new MemoryStream())
	            {
		            await responseStream.CopyToAsync(memoryStream);
		            var imageBytes = memoryStream.ToArray();
		            return Convert.ToBase64String(imageBytes);
	            }
            }
}
    }
    