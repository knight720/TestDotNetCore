using System.Text.Json;
using Amazon.DynamoDBv2.DocumentModel;
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

        public string GetTag(string tagId)
        {
            var table = this.GetTable();
            this._logger.LogInformation($"ItemCount: {table.Attributes.Count}");

            var document = table.GetItemAsync(new Primitive("TAG#1c9f54c59df9"), new Primitive("44#1699343119")).Result;

            return JsonSerializer.Serialize(document);
        }

        public bool SetTag()
        {
            var doc = new Document();
            var table = this.GetTable();
            table.PutItemAsync(doc);
            return true;
        }

        private Table GetTable()
        {
            return this._dynamoDBService.GetTable("Tags");
        }
    }
}