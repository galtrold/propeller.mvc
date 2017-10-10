using Sitecore.Data.Items;

namespace Propeller.Mvc.Model.Factory
{
    public class ModelFactory
    {

        public T Create<T>(Item dataItem) where T : IPropellerModel
        {
            var internalModelFactory = new InternalModelFactory();
            var vm = internalModelFactory.Create<T>(dataItem, null);
            return vm;
        }
    }
}