using System.Reflection;
using Propeller.Mvc.Model.Strategies;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Propeller.Mvc.Model.Factory
{
    internal class StringFieldStrategy : IFieldStrategy
    {
        public object CreateField(Item item, ID propertyId, PropertyInfo pi)
        {
            var field = item.Fields[propertyId];
            if (field != null)
                return field.Value;
            return string.Empty;
        }
    }
}