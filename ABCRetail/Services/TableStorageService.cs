using System;
using System.Threading.Tasks;
using Azure;
using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
namespace ABCRetail.Services
{
    public class TableStorageService
    {
        private readonly TableClient _tableClient;

        public TableStorageService(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("AzureStorage");

            var serviceClient = new TableServiceClient(connectionString);

            _tableClient = serviceClient.GetTableClient("Customers");
            _tableClient.CreateIfNotExists();
        }

        public async Task AddCustomerAsync(string name, string email)
        {
            var entity = new TableEntity(Guid.NewGuid().ToString(), email)
            {
                { "Name", name },
                { "Email", email }
            };

            await _tableClient.AddEntityAsync(entity);
        }
    }
}