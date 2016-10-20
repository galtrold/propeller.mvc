using Sitecore.Data;

namespace Propeller.Mvc.Core.Mapping
{
    /// <summary>
    /// Configuration item is used to pass information on in the configuration chain.
    /// </summary>
    public class ConfigurationItem
    {
        public ConfigurationItem(string fullPropertyName)
        {
            PropertyName = fullPropertyName;
        }

        public string PropertyName { get; set; }

        public ID FieldId { get; set; }
    }
}