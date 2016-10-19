using Propeller.Mvc.Configuration.Processing;
using Sitecore.Data;
using Sitecore.Diagnostics;

namespace Propeller.Mvc.Configuration.Utility
{
    public  static class PropertyIdResolver
    {
        internal static ID FetchId(string propertyName)
        {
            ID itemId;
            if (MappingTable.Instance.Map.TryGetValue(propertyName, out itemId))
                return itemId;

            Log.Warn("Failed to load property key", propertyName);
            return new ID("");
        }

    }
}