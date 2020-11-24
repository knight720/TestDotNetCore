using System.Collections.Generic;
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

        public string PK { get; internal set; }
        public string SK { get; internal set; }
        public string SalePageId { get; internal set; }
        public string MultipalLanguageContent { get; internal set; }
        public string TagName { get; internal set; }
        public string ShopId { get; internal set; }
    }
}