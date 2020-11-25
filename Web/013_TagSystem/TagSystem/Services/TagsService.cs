using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Microsoft.Extensions.Logging;
using TagSystem.Models.Tags;
using TagSystem.Services.DynamoDBs;

namespace TagSystem.Services
{
    public class TagsService : ITagsService
    {
        private const string TABLENAME = "Tags";
        private readonly IDynamoDBServcie _dynamoDBService;
        private readonly ILogger<TagsService> _logger;

        public TagsService(IDynamoDBServcie dynamoDBService, ILogger<TagsService> logger)
        {
            this._dynamoDBService = dynamoDBService;
            this._logger = logger;
        }

        public void Create(TagEntity tag)
        {
            var request = new PutItemRequest
            {
                TableName = TABLENAME,
                Item = tag.ToItem(),
            };
            var response = this._dynamoDBService.GetClient().PutItemAsync(request).Result;
        }

        public string GetTag(string tagId)
        {
            var table = this.GetTable();
            this._logger.LogInformation($"ItemCount: {table.Attributes.Count}");

            var document = table.GetItemAsync(new Primitive("TAG#1c9f54c59df9"), new Primitive("44#1699343119")).Result;

            return JsonSerializer.Serialize(document);
        }

        public IEnumerable<TagEntity> Query(TagQueryEntity queryEntity)
        {
            QueryRequest qRequest = new QueryRequest
            {
                TableName = TABLENAME,
                ExpressionAttributeNames = new Dictionary<string, string>
                {
                    { "#pk", "PK" }
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    { ":qPk",   new AttributeValue { S = queryEntity.PK} },
                },
                KeyConditionExpression = "#pk = :qPk",
            };

            var response = this._dynamoDBService.GetClient().QueryAsync(qRequest, default(CancellationToken)).Result;

            var result = response.Items.Select(i => new TagEntity(i)).ToList();

            return result;
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
            return this._dynamoDBService.GetTable(TABLENAME);
        }
    }
}