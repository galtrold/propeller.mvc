﻿using System.Reflection;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Propeller.Mvc.Model.Strategies
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