using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using Microsoft.Extensions.Configuration;

namespace ABCRetail.Services
{
    public class FileStorageService
    {
        private readonly ShareClient _shareClient;

        public FileStorageService(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("AzureStorage");

            _shareClient = new ShareClient(connectionString, "logs");
            _shareClient.CreateIfNotExists();
        }

        public async Task SaveLogAsync(string message)
        {
            var directoryClient = _shareClient.GetRootDirectoryClient();
            var fileClient = directoryClient.GetFileClient("log.txt");

            string logMessage = message + Environment.NewLine;
            byte[] byteArray = Encoding.UTF8.GetBytes(logMessage);

            using (var stream = new MemoryStream(byteArray))
            {
                // Create or overwrite file with correct size
                await fileClient.CreateAsync(byteArray.Length);

                // Upload content to file
                await fileClient.UploadRangeAsync(
                    ShareFileRangeWriteType.Update,
                    new HttpRange(0, byteArray.Length),
                    stream);
            }
        }
    }
}