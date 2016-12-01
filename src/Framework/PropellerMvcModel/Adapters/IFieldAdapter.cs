using Sitecore.Data;
using Sitecore.Data.Items;

namespace Propeller.Mvc.Model.Adapters
{
    public interface IFieldAdapter
    {
        void InitAdapter(Item item, ID propId);
    }
}