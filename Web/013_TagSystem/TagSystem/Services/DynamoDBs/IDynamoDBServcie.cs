using System.Threading.Tasks;
using Amazon.DynamoDBv2;

namespace TagSystem.Services.DynamoDBs
{
    public interface IDynamoDBServcie
    {
        bool CreateTable(string tableName);

        AmazonDynamoDBClient GetClient();

        Task<bool> TableExist(string tableName);
    }
}