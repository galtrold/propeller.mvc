using System.Reflection;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Propeller.Mvc.Model.Strategies
{
    internal interface IFieldStrategy
    {
        object CreateField(Item item, ID propertyId, PropertyInfo pi);
    }
}