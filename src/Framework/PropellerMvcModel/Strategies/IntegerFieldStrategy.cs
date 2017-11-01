using System.Reflection;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Propeller.Mvc.Model.Strategies
{
    class IntegerFieldStrategy : IFieldStrategy
    {
        public object CreateField(Item item, ID propertyId, PropertyInfo pi)
        {
            var field = item.Fields[propertyId];
            int intValue;
            if (int.TryParse(field.Value, out intValue))
                return intValue;
            return 0;
        }
    }
}