using Amazon.DynamoDBv2;

namespace TagSystem.Services
{
    public interface IDynamoDBServcie
    {
        bool CreateTable(string tableName);

        AmazonDynamoDBClient GetClient();
    }
}