using Sitecore.Data;

namespace Propeller.Mvc.Configuration.Mapping
{
    /// <summary>
    /// Configuration item is used to pass information on in the configuration chain.
    /// </summary>
    public class ConfigurationItem
    {
        public ConfigurationItem(string fullPropertyName, ID fieldId)
        {
            PropertyName = fullPropertyName;
            FieldId = fieldId;
        }

        public string PropertyName { get; set; }

        public ID FieldId { get; set; }
    }
}