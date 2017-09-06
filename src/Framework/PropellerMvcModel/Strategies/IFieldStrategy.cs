using System;
using System.Reflection;
using Propeller.Mvc.Model.Adapters;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Propeller.Mvc.Model.Strategies
{
    internal interface IFieldStrategy
    {
        object CreateField(Item item, ID propertyId, PropertyInfo pi);
    }

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