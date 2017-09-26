using System;
using System.Reflection;
using Propeller.Mvc.Model.Adapters;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Propeller.Mvc.Model.Strategies
{
    class AdapterFieldStrategy : IFieldStrategy
    {
        public object CreateField(Item item, ID propertyId, PropertyInfo pi)
        {

            var fieldAdapter = (IFieldAdapter)Activator.CreateInstance(pi.PropertyType);
            fieldAdapter.InitAdapter(item, propertyId);
            return fieldAdapter;
        }
    }
}