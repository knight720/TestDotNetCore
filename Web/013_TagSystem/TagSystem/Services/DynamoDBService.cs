using Amazon.DynamoDBv2;

namespace TagSystem.Services
{
    public class DynamoDBService
    {
        private static AmazonDynamoDBClient _client;

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
    }
}