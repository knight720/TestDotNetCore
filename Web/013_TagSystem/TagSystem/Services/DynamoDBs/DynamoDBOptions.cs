namespace TagSystem.Services.DynamoDBs
{
    public class DynamoDBOptions
    {
        public const string DynamoDB = "DynamoDB";

        public bool UseDynamoDbLocal { get; set; }
        public string ServiceUrl { get; set; }
    }
}