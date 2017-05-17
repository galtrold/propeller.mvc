using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Propeller.Mvc.Model.Adapters
{
    public class Image : IFieldAdapter
    {
        public string Url { get; set; }
        public string Alt { get; set; }

        public string Witdh { get; set; }

        public string Height { get; set; }

        public void InitAdapter(Item item, ID propId)
        {
            ImageField image = item.Fields[propId];

            var mediaItem = image.MediaDatabase.GetItem(image.MediaID);
            Url = Sitecore.Resources.Media.MediaManager.GetMediaUrl(mediaItem);
            Alt = image.Alt;
            Height = image.Height;
            Witdh = image.Width;


        }
    }
}