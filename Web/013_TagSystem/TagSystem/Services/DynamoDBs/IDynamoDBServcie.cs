using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

namespace TagSystem.Services.DynamoDBs
{
    public interface IDynamoDBServcie
    {
        void CreateTable(string tableName);

        Task<List<string>> TableList();

        AmazonDynamoDBClient GetClient();

        Task<bool> TableExist(string tableName);

        Table GetTable(string tableName);

        Task<QueryResponse> Query(string tableName, QueryRequest query);

        Task<PutItemResponse> PutItemAsync(PutItemRequest request);

        BatchWriteItemResponse BatchWriteItem(BatchWriteItemRequest request);

        Task<QueryResponse> QueryAsync(QueryRequest qRequest);

        void CreateTable(Models.DynamoDBs.DataModel dataModel);

        void DeleteTable(string tableName);
    }
}