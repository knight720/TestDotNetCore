using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

namespace TagSystem.Services.DynamoDBs
{
    public interface IDynamoDBServcie
    {
        bool CreateTable(string tableName);

        Task<List<string>> TableList();

        AmazonDynamoDBClient GetClient();

        Task<bool> TableExist(string tableName);

        Table GetTable(string tableName);

        Task<QueryResponse> Query(string tableName, QueryRequest query);

        Task<PutItemResponse> PutItemAsync(PutItemRequest request);

        Task<QueryResponse> QueryAsync(QueryRequest qRequest);
    }
}