using System.Threading;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Microsoft.Extensions.Logging;
using TagSystem.Services.DynamoDBs;

namespace TagSystem.Services
{
    public class TagsService : ITagsService
    {
        private readonly IDynamoDBServcie _dynamoDBService;
        private readonly ILogger<TagsService> _logger;

        public TagsService(IDynamoDBServcie dynamoDBService, ILogger<TagsService> logger)
        {
            this._dynamoDBService = dynamoDBService;
            this._logger = logger;
        }

        public bool GetTag(string tagId)
        {
            var query = new QueryRequest()
            {
            };
            //this._dynamoDBService.Query(query);

            var hash = new Primitive()
            {
            };

            var token = new CancellationToken();

            var table = this.GetTable();
            this._logger.LogInformation($"ItemCount: {table.Attributes.Count}");
            table.GetItemAsync(hash, token);

            return true;
        }

        private Table GetTable()
        {
            return this._dynamoDBService.GetTable("Tags");
        }
    }
}