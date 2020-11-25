using System.Collections.Generic;
using System.Text.Json.Serialization;
using Amazon.DynamoDBv2.Model;

namespace TagSystem.Models.Tags
{
    public class TagEntity
    {
        public TagEntity()
        {
        }

        public TagEntity(IDictionary<string, AttributeValue> item)
        {
            foreach (var i in item.Keys)
            {
                if (i == "PK")
                {
                    PK = item[i].S;
                }
                else if (i == "SK")
                {
                    SK = item[i].S;
                }
                else if (i == "SalePageId")
                {
                    SK = item[i].S;
                }
                else if (i == "MultipalLanguageContent")
                {
                    MultipalLanguageContent = item[i].S;
                }
                else if (i == "TagName")
                {
                    TagName = item[i].S;
                }
                else if (i == "ShopId")
                {
                    ShopId = item[i].S;
                }
            }
        }

        public Dictionary<string, AttributeValue> ToItem()
        {
            // Define item attributes
            Dictionary<string, AttributeValue> attributes = new Dictionary<string, AttributeValue>();
            // Author is hash-key
            attributes["PK"] = new AttributeValue { S = PK };
            // Title is range-key
            attributes["SK"] = new AttributeValue { S = SK };
            // Other attributes
            attributes["SalePageId"] = new AttributeValue { S = SalePageId };
            attributes["MultipalLanguageContent"] = new AttributeValue { S = MultipalLanguageContent };
            attributes["TagName"] = new AttributeValue { S = TagName };
            attributes["ShopId"] = new AttributeValue { S = ShopId };

            return attributes;
        }

        [JsonPropertyName("pk")]
        public string PK { get; set; }

        [JsonPropertyName("sk")]
        public string SK { get; set; }

        [JsonPropertyName("sale_page_id")]
        public string SalePageId { get; set; }

        [JsonPropertyName("multipal_language_content")]
        public string MultipalLanguageContent { get; set; }

        [JsonPropertyName("tag_name")]
        public string TagName { get; set; }

        [JsonPropertyName("shop_id")]
        public string ShopId { get; set; }
    }
}