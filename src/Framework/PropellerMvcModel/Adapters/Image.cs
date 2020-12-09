using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Propeller.Mvc.Model.Adapters
{
    public class Image : IFieldAdapter
    {
        public string Url { get; set; }
        public string Alt { get; set; }

        public void InitAdapter(Item item, ID propId)
        {
            Url = string.Empty;
            Alt = string.Empty;

            ImageField image = item.Fields[propId];
            if (image == null ||image.MediaID == ItemIDs.Null)
                return;
            
            var mediaItem = image.MediaDatabase.GetItem(image.MediaID);
            Url = Sitecore.Resources.Media.MediaManager.GetMediaUrl(mediaItem);
            Alt = image.Alt;
            

        }
    }
}