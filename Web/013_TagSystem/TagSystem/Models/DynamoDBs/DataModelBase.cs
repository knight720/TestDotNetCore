using Amazon.DynamoDBv2.Model;

namespace TagSystem.Models.DynamoDBs
{
    public class AttributeBase
    {
        public string AttributeName { get; set; }
        public string AttributeType { get; set; }

        public AttributeDefinition ToAttributeDefinition()
        {
            return new AttributeDefinition
            {
                AttributeName = this.AttributeName,
                AttributeType = this.AttributeType,
            };
        }
    }
}