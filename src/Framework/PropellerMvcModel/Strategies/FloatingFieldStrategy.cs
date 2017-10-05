using System.Reflection;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Propeller.Mvc.Model.Strategies
{
    internal class FloatingFieldStrategy : IFieldStrategy
    {
        public object CreateField(Item item, ID propertyId, PropertyInfo pi)
        {
            var field = item.Fields[propertyId];
            if (double.TryParse(field.Value, out var FloatingNumberValue))
                return FloatingNumberValue;
            return 0.0;
        }
    }
}