using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TagSystem.Services.DynamoDBs;

namespace TagSystem.Services
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
                if (this._options.UseDynamoDbLocal)
                {
                    var config = new AmazonDynamoDBConfig()
                    {
                        ServiceURL = this._options.ServiceUrl,
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
            this._configuration = configuration;
            this._logger = logger;

            this._options = new DynamoDBOptions();
            this._configuration.GetSection(DynamoDBOptions.DynamoDB).Bind(this._options);
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
                this.GetClient().CreateTableAsync(request);
            }
            catch (Exception ex)
            {
                this._logger.LogError(default(EventId), ex, "");
                response = false;
            }

            return response;
        }
    }
}