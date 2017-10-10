using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Propeller.Mvc.Core;
using Propeller.Mvc.Core.Processing;
using Propeller.Mvc.Model.Adapters;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Search.Crawlers.FieldCrawlers;

namespace Propeller.Mvc.Model.Factory
{
    internal class InternalModelFactory
    {
        /// <summary>
        /// The actual create method. The method is hidden(private) in order to hide the second type parameter because the client will never have to use it.
        /// It is only used during resursive calls when building the object tree.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataItem"></param>
        /// <param name="implicitViewModelType"></param>
        /// <returns></returns>
        public T Create<T>(Item dataItem, Type implicitViewModelType) where T : IPropellerModel
        {
            var viewModelType = implicitViewModelType == null ? typeof(T) : implicitViewModelType;
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
                        pi.SetValue(viewModel, Create<IPropellerModel>(viewModelItem, pi.PropertyType), null);
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