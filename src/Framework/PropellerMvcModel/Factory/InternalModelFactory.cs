using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Propeller.Mvc.Core;
using Propeller.Mvc.Core.Processing;
using Propeller.Mvc.Model.Adapters;
using Sitecore.Common;
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

                ID sitecoreFieldId;
                if (MappingTable.Instance.IncludeMap.TryGetValue(propertyIdentifier, out sitecoreFieldId))
                {
                    Type chieldViewModelType;
                    if (MappingTable.Instance.ViewModelRegistry.TryGetValue(pi.PropertyType.FullName, out chieldViewModelType))
                    {
                        // Property is a single propeller models
                        var viewModelItem = GetReferencedItem(dataItem, sitecoreFieldId);
                        pi.SetValue(viewModel, Create<IPropellerModel>(viewModelItem, pi.PropertyType), null);
                    }
                    else if (typeof(IEnumerable<IPropellerModel>).IsAssignableFrom(pi.PropertyType))
                    {

                        // Property is a collection of propeller models
                        var modelItems = GetListItems(dataItem, sitecoreFieldId);

                        var genericType = pi.PropertyType.GetGenericArguments().FirstOrDefault();
                        if (genericType == null)
                            continue;

                        var propellerModelCollection = CreateCollection(modelItems, genericType);

                        pi.SetValue(viewModel, propellerModelCollection);
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

        public object CreateCollection(IEnumerable<Item> modelItemList, Type modelType)
        {
            var modelCollecitonType = typeof(List<>).MakeGenericType(modelType);
            var propellerModelList = (IList)Activator.CreateInstance(modelCollecitonType);

            foreach (var modelItem in modelItemList)
            {

                try
                {
                    propellerModelList.Add(Create<IPropellerModel>(modelItem, modelType));
                }
                catch (Exception e)
                {
                    Log.Error(e.Message, e, this);
                    throw;
                }

            }

            return propellerModelList;
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
        private IEnumerable<Item> GetListItems(Item item, ID propertyId)
        {

            if (propertyId == ID.Null)
                return null;

            MultilistField itemList = item.Fields[propertyId];
            return itemList?.GetItems() ?? new Item[] { };
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