using System.Reflection;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Propeller.Mvc.Model.Strategies
{
    class BooleanFieldStrategy : IFieldStrategy
    {
        

        public object CreateField(Item item, ID propertyId, PropertyInfo pi)
        {
            var field = item.Fields[propertyId];
            if (field != null)
            {
                var value = field.Value;
                return value == "1";
            }
            return false;
        }
    }
}