using Propeller.Mvc.Model;
using Propeller.Mvc.Model.Factory;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Presentation;

namespace Propeller.Mvc.Presentation.Factory
{
    public class ViewModelFactory : ModelFactory
    {
        public T Create<T>(Rendering rendering) where T : IPropellerModel
        {
            if(rendering.Item != null)
                return Create<T>(rendering.Item);

            if (PageContext.Current.Item != null)
                return Create<T>(PageContext.Current.Item);

            Log.Warn("Error no data item available", this);
            return default(T);

        }
    }
}