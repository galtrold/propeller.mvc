﻿using Propeller.Mvc.Configuration.Processing;
using Propeller.Mvc.Configuration.Utility;

namespace Propeller.Mvc.Configuration.Mapping
{
    /// <summary>
    /// Extension methods for all the configuration types provided for the ConfigurationItem
    /// </summary>
    public static class ConfigurationMethods
    {

        public static ConfigurationItem Map(this ConfigurationItem configItem)
        {
            if (!MappingTable.Instance.Map.ContainsKey(configItem.PropertyName))
                MappingTable.Instance.Map.Add(configItem.PropertyName, configItem.FieldId);

            if (!MappingTable.Instance.JumpMap.ContainsKey(configItem.PropertyName))
                MappingTable.Instance.JumpMap.Add(configItem.PropertyName, () => PropertyIdResolver.FetchId(configItem.PropertyName));

            return configItem;
        }

        public static ConfigurationItem Include(this ConfigurationItem configItem)
        {
            if (!MappingTable.Instance.IncludeMap.ContainsKey(configItem.PropertyName))
                MappingTable.Instance.IncludeMap.Add(configItem.PropertyName, configItem.FieldId);

            return configItem;

        }

        public static ConfigurationItem Editable(this ConfigurationItem configItem)
        {
            if (!MappingTable.Instance.IncludeMap.ContainsKey(configItem.PropertyName))
                MappingTable.Instance.IncludeMap.Add(configItem.PropertyName, configItem.FieldId);
            if (!MappingTable.Instance.EditableMap.ContainsKey(configItem.PropertyName))
                MappingTable.Instance.EditableMap.Add(configItem.PropertyName, configItem.FieldId);

            return configItem;

        }

       
    }
}

