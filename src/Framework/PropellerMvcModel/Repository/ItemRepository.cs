using System.Collections.Generic;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Propeller.Mvc.Model.ItemTools
{
    public class ItemRepository
    {
        public Item GetReferencedItem(Item item, ID propertyId)
        {
            var field = item.Fields[propertyId];
            Item targetItem = null;
            ReferenceField refItem = field;
            LinkField linkField = field;


            if (refItem != null && refItem.TargetItem != null)
            {
                targetItem = refItem.TargetItem;
            }
            else if (linkField != null && linkField.TargetItem != null)
            {
                targetItem = linkField.TargetItem;
            }

            if (targetItem == null && field != null && !string.IsNullOrWhiteSpace(field.Value))
            {
                Log.Warn($"Propeller ModelFactory failed to get referenced Sitecore item (ITEM id: {item.ID}, Field id:{propertyId} Field value: '{field.Value}')", this);
            }

            return targetItem;
        }

        public IEnumerable<Item> GetItemList(Item item, ID propertyId)
        {

            if (propertyId == ID.Null)
                return null;

            MultilistField itemList = item.Fields[propertyId];
            return itemList?.GetItems() ?? new Item[] { };
        }
    }
}