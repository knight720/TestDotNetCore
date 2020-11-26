using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Amazon.DynamoDBv2;
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

            var response = this._dynamoDBService.PutItemAsync(request).Result;
        }

        public void CreateList(IEnumerable<TagEntity> tags)
        {
            // Construct write-request for first table
            List<WriteRequest> TagItems = new List<WriteRequest>();
            tags.ToList().ForEach(i => TagItems.Add(new WriteRequest { PutRequest = new PutRequest { Item = i.ToItem() } }));

            // Construct table-keys mapping
            Dictionary<string, List<WriteRequest>> requestItems = new Dictionary<string, List<WriteRequest>>();
            requestItems[TABLENAME] = TagItems;

            var request = new BatchWriteItemRequest { RequestItems = requestItems };

            var response = this._dynamoDBService.BatchWriteItem(request);
        }

        public string GetTag(string tagId)
        {
            var table = this.GetTable();
            this._logger.LogInformation($"ItemCount: {table.Attributes.Count}");

            var document = table.GetItemAsync(new Primitive("TAG#1c9f54c59df9"), new Primitive("44#1699343119")).Result;

            return JsonSerializer.Serialize(document);
        }

        public IEnumerable<TagEntity> GetTag(string shopId, string id)
        {
            var values = new Dictionary<string, AttributeValue>
            {
                { ":qShopId", new AttributeValue { S = shopId } },
            };
            var expression = "ShopId = :qShopId";

            if (string.IsNullOrEmpty(id) == false)
            {
                values.Add(":qPk", new AttributeValue { S = id });
                expression = $"{expression} AND PK = :qPk";
            }

            QueryRequest qRequest = new QueryRequest
            {
                TableName = TABLENAME,
                IndexName = "ByShop",
                ExpressionAttributeValues = values,
                KeyConditionExpression = expression,
            };

            var response = this._dynamoDBService.QueryAsync(qRequest).Result;

            return response.Items.Select(i => new TagEntity(i)).ToList();
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
                    { ":qPk", new AttributeValue { S = queryEntity.PK } },
                },
                KeyConditionExpression = "#pk = :qPk",
            };

            var response = this._dynamoDBService.QueryAsync(qRequest).Result;

            return response.Items.Select(i => new TagEntity(i)).ToList();
        }

        private AmazonDynamoDBClient GetClient()
        {
            return this._dynamoDBService.GetClient();
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