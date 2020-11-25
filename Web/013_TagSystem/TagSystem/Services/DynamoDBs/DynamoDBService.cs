using System;
using System.Collections.Generic;
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
        public bool CreateTable(string tableName)
        {
            var response = true;

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

            try
            {
                GetClient().CreateTableAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(default, ex, "");
                response = false;
            }

            return response;
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

        public async Task<QueryResponse> QueryAsync(QueryRequest qRequest)
        {
            return await GetClient().QueryAsync(qRequest, default);
        }
    }
}