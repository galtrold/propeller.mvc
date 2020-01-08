using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Propeller.Mvc.Core;
using Propeller.Mvc.Core.Processing;
using Propeller.Mvc.Model.Adapters;
using Propeller.Mvc.Model.ItemTools;
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
        // VisitedComplete dictionary is use to detect back edges. i.e. cyclic references.
        private Dictionary<string, IPropellerModel> _visitedCompleted = new Dictionary<string, IPropellerModel>();

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
            var viewModel = (T)Activator.CreateInstance(viewModelType);
            if (dataItem == null)
                return viewModel;
            var  modelId = $"{viewModelType}-{dataItem.ID}";
            _visitedCompleted.Add(modelId, viewModel);
            
            viewModel.DataItem = dataItem;
            var itemRepository = new ItemRepository();

            foreach (var pi in viewModelType.GetProperties())
            {
                var propertyIdentifier = $"{viewModelType.FullName}.{pi.Name}";

                ID sitecoreFieldId;
                Func<ID> idFunc;
                if (MappingTable.Instance.ViewModelRegistry.ContainsKey(pi.PropertyType.FullName) && MappingTable.Instance.JumpMap.TryGetValue(propertyIdentifier, out idFunc))
                {

                    // Property is a single propeller model i.e. a reference property.
                    var viewModelItem = itemRepository.GetReferencedItem(dataItem, idFunc());
                    var propertyModelId = $"{pi.PropertyType.FullName}-{viewModelItem.ID}";
                    // Before creating referenced item check if we do not have a back edge.
                    IPropellerModel ancestor;
                    if (viewModelItem != null && _visitedCompleted.TryGetValue(propertyModelId, out ancestor))
                    {
                        try
                        {
                            pi.SetValue(viewModel, ancestor, null);
                        }
                        catch (Exception e)
                        {
                            
                        }
                        
                    }
                    else if(viewModelItem != null)
                    {
                        var vm = Create<IPropellerModel>(viewModelItem, pi.PropertyType);
                        pi.SetValue(viewModel, vm, null);
                    }
                   
                    
                    
                }
                else if (typeof(IEnumerable<IPropellerModel>).IsAssignableFrom(pi.PropertyType) && MappingTable.Instance.JumpMap.TryGetValue(propertyIdentifier, out idFunc))
                {

                    // Property is a collection of propeller models
                    var modelItems = itemRepository.GetItemList(dataItem, idFunc());
                    var genericType = pi.PropertyType.GetGenericArguments().FirstOrDefault();
                    if (genericType == null)
                        continue;

                    var propellerModelCollection = CreateCollection(modelItems, genericType);

                    pi.SetValue(viewModel, propellerModelCollection);
                }
                else if (MappingTable.Instance.IncludeMap.TryGetValue(propertyIdentifier, out idFunc))
                {

                    sitecoreFieldId = idFunc();
                    // Property is a value
                    var fieldValue = ParseFieldValue(pi, dataItem, sitecoreFieldId);
                    pi.SetValue(viewModel, fieldValue);
                }
            }
            viewModel.Init();
            _visitedCompleted[dataItem.ID.ToString()] = viewModel;
            return viewModel;
        }

        private object CreateCollection(IEnumerable<Item> modelItemList, Type modelType)
        {
            var modelCollecitonType = typeof(List<>).MakeGenericType(modelType);
            var propellerModelList = (IList)Activator.CreateInstance(modelCollecitonType);

            foreach (var modelItem in modelItemList)
            {

                try
                {
                    var model = Create<IPropellerModel>(modelItem, modelType);
                    model.Init();
                    propellerModelList.Add(model);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message, e, this);
                    throw;
                }

            }

            return propellerModelList;
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