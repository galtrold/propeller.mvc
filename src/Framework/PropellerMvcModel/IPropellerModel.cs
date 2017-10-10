using Sitecore.Data.Items;

namespace Propeller.Mvc.Model
{
    public interface IPropellerModel
    {
        Item DataItem { get; set; }
    }
}