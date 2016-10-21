using System.Runtime.InteropServices;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Propeller.Mvc.Model.Adapters
{
    public interface IFieldAdapter
    {
        void InitAdapter(Item item, ID propId);
    }

    public class GeneralLink : IFieldAdapter
    {
        public string Url { get; set; }

        public string Desciption { get; set; }

        public void InitAdapter(Item item, ID propId)
        {

            LinkField lf = item.Fields[propId];

            Desciption = lf.Text;
            switch (lf.LinkType.ToLower())
            {
                case "internal":
                    // Use LinkMananger for internal links, if link is not empty
                    Url = lf.TargetItem != null ? Sitecore.Links.LinkManager.GetItemUrl(lf.TargetItem) : string.Empty;
                    break;
                case "media":
                    // Use MediaManager for media links, if link is not empty
                    Url = lf.TargetItem != null ? Sitecore.Resources.Media.MediaManager.GetMediaUrl(lf.TargetItem) : string.Empty;
                    break;
                case "external":
                    // Just return external links
                    Url = lf.Url;
                    break;
                case "anchor":
                    // Prefix anchor link with # if link if not empty
                    Url = !string.IsNullOrEmpty(lf.Anchor) ? "#" + lf.Anchor : string.Empty;
                    break;
                case "mailto":
                    // Just return mailto link
                    Url = lf.Url;
                    break;
                case "javascript":
                    // Just return javascript
                    Url = lf.Url;
                    break;
                default:
                    // Just please the compiler, this
                    // condition will never be met
                    Url = lf.Url;
                    break;
            }
        }

        
    }
}