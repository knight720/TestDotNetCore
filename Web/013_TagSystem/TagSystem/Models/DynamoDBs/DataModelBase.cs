using System;
using System.Diagnostics.CodeAnalysis;
using Amazon.DynamoDBv2.Model;

namespace TagSystem.Models.DynamoDBs
{
    public class AttributeBase : IEquatable<AttributeBase>
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

        public bool Equals([AllowNull] AttributeBase other)
        {
            //Check whether the compared object is null.
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal.
            return AttributeName.Equals(other.AttributeName) && AttributeType.Equals(other.AttributeType);
        }

        public override int GetHashCode()
        {
            // Get hash code for the Name field if it is not null.
            int hashName = AttributeName == null ? 0 : AttributeName.GetHashCode();

            //Get hash code for the Code field.
            int hashType = AttributeType == null ? 0 : AttributeType.GetHashCode();

            //Calculate the hash code for the product.
            return hashName ^ hashType;
        }
    }
}