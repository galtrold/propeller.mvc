using Propeller.Mvc.Core.Processing;
using Sitecore.Data;
using Sitecore.Diagnostics;

namespace Propeller.Mvc.Core.Utility
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