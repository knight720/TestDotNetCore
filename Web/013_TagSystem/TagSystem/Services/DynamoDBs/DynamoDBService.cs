﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TagSystem.Services.DynamoDBs
{
    public class DynamoDBService : IDynamoDBServcie
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DynamoDBService> _logger;

        private static AmazonDynamoDBClient _client;
        private readonly DynamoDBOptions _options;

        public AmazonDynamoDBClient GetClient()
        {
            if (_client == null)
            {
                if (_options.UseDynamoDbLocal)
                {
                    var config = new AmazonDynamoDBConfig()
                    {
                        ServiceURL = _options.ServiceUrl,
                    };
                    _client = new AmazonDynamoDBClient(config);
                }
                else
                {
                    _client = new AmazonDynamoDBClient();
                }
            }
            return _client;
        }

        public DynamoDBService(IConfiguration configuration, ILogger<DynamoDBService> logger)
        {
            _configuration = configuration;
            _logger = logger;

            _options = new DynamoDBOptions();
            _configuration.GetSection(DynamoDBOptions.DynamoDB).Bind(_options);
        }

        /// <summary>
        /// Creates the table.
        /// </summary>
        /// <returns></returns>
        public void CreateTable(string tableName)
        {
            var request = new CreateTableRequest
            {
                TableName = tableName,
                AttributeDefinitions = new List<AttributeDefinition>()
                {
                    new AttributeDefinition
                    {
                        AttributeName = "Id",
                        AttributeType = "N"
                    }
                },
                KeySchema = new List<KeySchemaElement>()
                {
                    new KeySchemaElement
                    {
                        AttributeName = "Id",
                        KeyType = "HASH"  //Partition key
                    }
                },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 10,
                    WriteCapacityUnits = 5
                },
            };

            GetClient().CreateTableAsync(request).Wait();
        }

        public void CreateTable(Models.DynamoDBs.DataModel dataModel)
        {
            var request = new CreateTableRequest
            {
                TableName = dataModel.TableName,
                ProvisionedThroughput = new ProvisionedThroughput(1, 1),
            };

            var attributes = new List<Models.DynamoDBs.AttributeBase>();

            request.KeySchema.Add(dataModel.KeyAttributes.PartitionKey.ToKeySchemaElement());
            attributes.Add(dataModel.KeyAttributes.PartitionKey);

            if (string.IsNullOrWhiteSpace(dataModel.KeyAttributes.SortKey?.AttributeName) == false)
            {
                request.KeySchema.Add(dataModel.KeyAttributes.SortKey.ToKeySchemaElement());
                attributes.Add(dataModel.KeyAttributes.SortKey);
            }

            foreach (var i in dataModel.GlobalSecondaryIndexes)
            {
                var gsi = new GlobalSecondaryIndex
                {
                    IndexName = i.IndexName,
                    Projection = new Projection
                    {
                        ProjectionType = ProjectionType.ALL,
                    },
                    ProvisionedThroughput = new ProvisionedThroughput(1, 1),
                };

                gsi.KeySchema.Add(i.KeyAttributes.PartitionKey.ToKeySchemaElement());
                gsi.KeySchema.Add(i.KeyAttributes.SortKey.ToKeySchemaElement());

                request.GlobalSecondaryIndexes.Add(gsi);

                attributes.Add(i.KeyAttributes.PartitionKey);
                attributes.Add(i.KeyAttributes.SortKey);
            }

            request.AttributeDefinitions.AddRange(attributes.Distinct().Select(i => i.ToAttributeDefinition()));

            GetClient().CreateTableAsync(request).Wait();
        }

        public void DeleteTable(string tableName)
        {
            var response = true;

            var request = new DeleteTableRequest
            {
                TableName = tableName,
            };

            GetClient().DeleteTableAsync(request).Wait();
        }

        public async Task<bool> TableExist(string tableName)
        {
            var response = await this.TableList();
            return response.Contains(tableName);
        }

        public Table GetTable(string tableName)
        {
            return Table.LoadTable(GetClient(), tableName);
        }

        public async Task<QueryResponse> Query(string tableName, QueryRequest query)
        {
            return await GetClient().QueryAsync(query);
        }

        public async Task<List<string>> TableList()
        {
            var response = await GetClient().ListTablesAsync();
            return response.TableNames;
        }

        public async Task<PutItemResponse> PutItemAsync(PutItemRequest request)
        {
            return await this.GetClient().PutItemAsync(request);
        }

        public BatchWriteItemResponse BatchWriteItem(BatchWriteItemRequest request)
        {
            BatchWriteItemResponse result;
            do
            {
                // Issue request and retrieve items
                result = GetClient().BatchWriteItemAsync(request).Result;

                // Some items may not have been processed!
                //  Set RequestItems to the result's UnprocessedItems and reissue request
                request.RequestItems = result.UnprocessedItems;
            } while (result.UnprocessedItems.Count > 0);

            return result;
        }

        public async Task<QueryResponse> QueryAsync(QueryRequest qRequest)
        {
            return await GetClient().QueryAsync(qRequest, default);
        }
    }
}