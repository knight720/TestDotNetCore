using Amazon.DynamoDBv2.Model;
using TagSystem.Services.DynamoDBs;

namespace TagSystem.Services
{
    public class TagsService : ITagsService
    {
        private readonly IDynamoDBServcie _dynamoDBService;

        public TagsService(IDynamoDBServcie dynamoDBService)
        {
            this._dynamoDBService = dynamoDBService;
        }

        public bool GetTag(string tagId)
        {
            var query = new QueryRequest();
            this._dynamoDBService.Query(query);
            return true;
        }
    }
}