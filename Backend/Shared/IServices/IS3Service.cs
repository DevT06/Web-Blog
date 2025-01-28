    namespace Shared.IServices
    {
        public interface IS3Service
        {
            Task UploadFileAsync(string bucketName, string key, Stream inputStream);
            Task<Stream> GetFileAsync(string bucketName, string key);
            Task DeleteFileAsync(string bucketName, string key);

            Task<string> GetImageBase64Async(string bucketName, string key);
}
    }
    