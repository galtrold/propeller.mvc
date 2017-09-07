using System;
using System.Linq;
using System.Reflection;
using Propeller.Mvc.Core.Processing;
using Propeller.Mvc.Model.Adapters;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Search.Crawlers.FieldCrawlers;

namespace Propeller.Mvc.Model.Factory
{
    public class ModelFactory
    {
        public T Create<T>(Item dataItem) where T : IPropellerModel
        {
            var viewModelType = typeof(T);
            if (dataItem == null)
            {
                return default(T);
            }

            var viewModel = (T)Activator.CreateInstance(viewModelType);
            viewModel.DataItem = dataItem;

            foreach (var pi in viewModelType.GetProperties())
            {
                var propertyIdentifier = $"{viewModelType.FullName}.{pi.Name}";

                if (MappingTable.Instance.IncludeMap.TryGetValue(propertyIdentifier, out var sitecoreFieldId))
                {
                    if (MappingTable.Instance.ViewModelRegistry.TryGetValue(pi.PropertyType.FullName, out var chieldViewModelType))
                    {
                        // Property is a viewmodel
                        var viewModelItem = GetReferencedItem(dataItem, sitecoreFieldId);
                        pi.SetValue(viewModel, this.Create<IPropellerModel>(viewModelItem), null);
                    }
                    else
                    {
                        // Property is a value
                        var fieldValue = ParseFieldValue(pi, dataItem, sitecoreFieldId);
                        pi.SetValue(viewModel, fieldValue);
                    }
                }
            }

            return viewModel;
        }

        private Item GetReferencedItem(Item item, ID propertyId)
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

            return targetItem;
        }

        private object ParseFieldValue(PropertyInfo propertyInfo, Item item, ID propertiId)
        {

            var factory = new FieldFactory();

            var strategy = factory.GetFieldStrategy(propertyInfo.PropertyType);

            object result = strategy.CreateField(item, propertiId, propertyInfo);

            return result;

        }

    }
}