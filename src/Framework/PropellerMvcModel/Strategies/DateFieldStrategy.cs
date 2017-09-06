using System;
using System.Reflection;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Propeller.Mvc.Model.Strategies
{
    class DateFieldStrategy : IFieldStrategy
    {
        public object CreateField(Item item, ID propertyId, PropertyInfo pi)
        {
            var field = item.Fields[propertyId];
            var dateField = (DateField)field;
            if (dateField != null)
                return dateField.DateTime;
            return DateTime.MinValue;
        }
    }
}