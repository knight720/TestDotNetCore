﻿using System.Collections.Generic;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace TagSystem.Models.DynamoDBs
{
    public class Table
    {
        public string ModelName { get; set; }
        public ModelMetadata ModelMetadata { get; set; }
        public List<DataModel> DataModel { get; set; }
    }

    public class ModelMetadata
    {
        public string Author { get; set; }
        public string DateCreated { get; set; }
        public string DateLastModified { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string AWSService { get; set; }
    }

    public class PartitionKey : AttributeBase
    {
        public KeySchemaElement ToKeySchemaElement()
        {
            return new KeySchemaElement
            {
                AttributeName = this.AttributeName,
                KeyType = KeyType.HASH,
            };
        }
    }

    public class SortKey : AttributeBase
    {
        public KeySchemaElement ToKeySchemaElement()
        {
            return new KeySchemaElement
            {
                AttributeName = this.AttributeName,
                KeyType = KeyType.RANGE,
            };
        }
    }

    public class KeyAttributes
    {
        public PartitionKey PartitionKey { get; set; }
        public SortKey SortKey { get; set; }


    }

    public class NonKeyAttribute : AttributeBase
    {
    }

    public class Projection
    {
        public string ProjectionType { get; set; }
    }

    public class GlobalSecondaryIndex
    {
        public string IndexName { get; set; }
        public KeyAttributes KeyAttributes { get; set; }
        public Projection Projection { get; set; }
    }

    public class PK
    {
        public string S { get; set; }
    }

    public class SK
    {
        public string S { get; set; }
    }

    public class TagName
    {
        public string S { get; set; }
    }

    public class MultipalLanguageContent
    {
        public string S { get; set; }
    }

    public class ShopId
    {
        public string S { get; set; }
    }

    public class SalePageId
    {
        public string S { get; set; }
    }

    public class TableData
    {
        public PK PK { get; set; }
        public SK SK { get; set; }
        public TagName TagName { get; set; }
        public MultipalLanguageContent MultipalLanguageContent { get; set; }
        public ShopId ShopId { get; set; }
        public SalePageId SalePageId { get; set; }
    }

    public class MySql
    {
    }

    public class DataAccess
    {
        public MySql MySql { get; set; }
    }

    public class DataModel
    {
        public string TableName { get; set; }
        public KeyAttributes KeyAttributes { get; set; }
        public List<NonKeyAttribute> NonKeyAttributes { get; set; }
        public List<GlobalSecondaryIndex> GlobalSecondaryIndexes { get; set; }
        public List<TableData> TableData { get; set; }
        public DataAccess DataAccess { get; set; }
    }
}