using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Microsoft.Extensions.Logging;

namespace TagSystem.Services
{
    public class DynamoDBService : IDynamoDBServcie
    {
        private static AmazonDynamoDBClient _client;
        private readonly ILogger<DynamoDBService> _logger;

        public AmazonDynamoDBClient GetClient()
        {
            if (_client == null)
            {
                AmazonDynamoDBConfig ddbConfig = new AmazonDynamoDBConfig();
                ddbConfig.ServiceURL = "http://localhost:8000";
                _client = new AmazonDynamoDBClient(ddbConfig);
            }
            return _client;
        }

        public DynamoDBService(ILogger<DynamoDBService> logger)
        {
            this._logger = logger;
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