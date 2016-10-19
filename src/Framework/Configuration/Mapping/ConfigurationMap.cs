using System;
using System.Linq.Expressions;
using Propeller.Mvc.Configuration.Processing;
using Propeller.Mvc.Configuration.Utility;
using Sitecore.Data;
using Sitecore.Diagnostics;

namespace Propeller.Mvc.Configuration.Mapping
{
    public class ConfigurationMap<T>
    {
        public ConfigurationItem Property(Expression<Func<T, object>> expression, ID fieldId)
        {
            var fullyQualifiedClassName = typeof(T).FullName;
            var propertyName = GetPropertyNameFromExpressionService.Get(expression);
            var fullPropertyName = string.Format("{0}.{1}", fullyQualifiedClassName, propertyName);
            

            return new ConfigurationItem(fullPropertyName, fieldId);
        }

        public void ImportConfiguration(Type type)
        {
            try
            {
                if (!type.IsInterface)
                    return;

                var fullyQualifiedClassName = typeof(T).FullName;
                var interfaceFullyQualifiedClassName = type.FullName;
                
                var propertyInfos = type.GetProperties();
                foreach (var propertyInfo in propertyInfos)
                {
                    var fullPropertyName = string.Format("{0}.{1}", fullyQualifiedClassName, propertyInfo.Name);
                    var interfaceFullPropertyName = string.Format("{0}.{1}", interfaceFullyQualifiedClassName, propertyInfo.Name);
                    if (!MappingTable.Instance.JumpMap.ContainsKey(fullyQualifiedClassName))
                        MappingTable.Instance.JumpMap.Add(fullPropertyName, () => PropertyIdResolver.FetchId(interfaceFullPropertyName));

                }


            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, this);
            }
        }

       
    }
}