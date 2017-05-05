using Sitecore.Data;
using Sitecore.Data.Items;

namespace Propeller.Mvc.Model.Adapters
{
    public class Checkbox : IFieldAdapter
    {
        public bool IsChecked { get; set; }

        public void InitAdapter(Item item, ID propId)
        {
            var checkboxValue = item.Fields[propId].Value;
            IsChecked = checkboxValue == "1";
        }
    }
}