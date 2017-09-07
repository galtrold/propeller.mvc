using System;
using System.Linq;
using System.Reflection;
using Propeller.Mvc.Model.Adapters;
using Propeller.Mvc.Model.Strategies;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Propeller.Mvc.Model.Factory
{
    internal class FieldFactory
    {
        public IFieldStrategy GetFieldStrategy(Type propertyType)
        {

            if (propertyType == typeof(string))
                return new StringFieldStrategy();

            if (propertyType == typeof(int))
                return new IntegerFieldStrategy();

            if (propertyType == typeof(bool))
                return new BooleanFieldStrategy();

            if (propertyType == typeof(DateTime))
                return new DateFieldStrategy();

            if (propertyType.GetInterfaces().Contains(typeof(IFieldAdapter)))
                return new AdapterFieldStrategy();

                return new EmptyFieldStrategy();
        }
    }

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