using System;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Propeller.Mvc.Model.Adapters
{
    public class Integer : IFieldAdapter
    {
        public int IntValue { get; set; }

        public void InitAdapter(Item item, ID propId)
        {
            
            var integerStrValue = item.Fields[propId].Value;
            if (string.IsNullOrWhiteSpace(integerStrValue))
            {
                IntValue = 0;
                return;
            }

            int intValue;
            if (!int.TryParse(integerStrValue, out intValue))
                throw new ArgumentException(string.Format("Field Adapter 'Integer' could not read integer value. Actual value: {0}", integerStrValue));
        }
    }
}