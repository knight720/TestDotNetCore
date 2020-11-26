using System.Collections.Generic;
using System.Linq;
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
            var properties = this.GetType().GetProperties();

            foreach (var i in item.Keys)
            {
                var property = properties.FirstOrDefault(p => p.Name == i);
                property?.SetValue(this, item[i].S);
            }
        }

        public Dictionary<string, AttributeValue> ToItem()
        {
            // Define item attributes
            Dictionary<string, AttributeValue> attributes = new Dictionary<string, AttributeValue>();

            foreach (var property in this.GetType().GetProperties())
            {
                var value = property.GetValue(this)?.ToString();
                if (string.IsNullOrEmpty(value) == false)
                {
                    attributes[property.Name] = new AttributeValue { S = value };
                }
            }

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

        [JsonPropertyName("tag_id")]
        public string TagId { get; set; }
    }
}