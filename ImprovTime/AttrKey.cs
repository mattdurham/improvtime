using System;

namespace ImprovTime
{
    public struct AttrKey
    {
        /// <summary>
        /// Example would be "VERB"
        /// </summary>
        public string AttributeType { get; set; }
        
        /// <summary>
        /// Example would be "Get" or "Post"
        /// </summary>
        public string AttributeValue { get; set; }
    
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            var otherKey = (AttrKey) obj;
            return otherKey.AttributeType == AttributeType && otherKey.AttributeValue == AttributeValue;
        }

      

        public override int GetHashCode()
        {
            return HashCode.Combine(AttributeType, AttributeValue);
        }
    }
}