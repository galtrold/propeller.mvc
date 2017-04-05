﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Propeller.Mvc.Core;
using Propeller.Mvc.Core.Processing;
using Propeller.Mvc.Model.Adapters;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;


namespace Propeller.Mvc.Model
{
    public class PropellerModel<T> : PropellerEntity<T>, IPropellerModel where T : IPropellerModel, new()
    {
        public PropellerModel() { }

        public PropellerModel(Item dataItem) : base(dataItem)
        {
            if (dataItem != null)
                IncludeRawValues(dataItem);
        }

        /// <summary>
        /// DropList is not supported ny this method. DropList are generelly not that useful lsince they only provide the selected Item.Name 
        /// Use DropLink instead. 
        /// </summary>
        /// <typeparam name="TP"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public TP GetItemReference<TP>(Expression<Func<T, object>> expression) where TP : PropellerEntity<TP>, new()
        {
            var type = typeof(TP);
            Item targetItem;
            var item = DataItem;
            if (item == null)
                return Activator.CreateInstance(type, null) as TP;

            var propId = GetPropertyId(expression);
            if (propId ==  ID.Null)
                return Activator.CreateInstance(type, null) as TP;

            var fieldItem = item.Fields[propId];
            if(fieldItem == null)
                return Activator.CreateInstance(type, null) as TP;

            if (fieldItem.Type.ToLower().Equals("droplist"))
            {
                Log.Warn("DropList is not supported ny this method. DropList are generelly not that useful lsince they only provide the selected Item.Name. Use DropLink instead. ", item);
                targetItem = null;
            }
            else
            {
                ReferenceField selectedItem = item.Fields[propId];
                if (selectedItem == null)
                {
                    return Activator.CreateInstance(type, null) as TP;
                }
                targetItem = selectedItem.TargetItem;
            }

            
            return Activator.CreateInstance(type, targetItem) as TP;
            
        }

        public IEnumerable<TK> GetList<TK>(Expression<Func<T, object>> expression) where TK : PropellerEntity<TK>, new()
        {

            if (DataItem == null)
                return new List<TK>();

            var propId = GetPropertyId(expression);
            if (propId == ID.Null)
                return new List<TK>();

            var listField = DataItem.Fields[propId];

            var database = DataItem.Database;

            var listItemIds = listField.Value.Split('|');

            if (string.IsNullOrWhiteSpace(listField.Value) || listItemIds.Length < 1)
                return new List<TK>();
            var type = typeof(TK);

            var results = new List<TK>();
            foreach (var itemId in listItemIds)
            {
                var viewModelItem = (TK)Activator.CreateInstance(type, database.GetItem(itemId));
                results.Add(viewModelItem);
            }

            return results;
        }

        public CT GetAs<CT>(Expression<Func<T, object>> expression) where CT : IFieldAdapter, new()
        {
            try
            {
                var item = DataItem;
                var propId = GetPropertyId(expression);
                if(propId == ID.Null)
                    return new CT();

                var adapter = new CT();
                adapter.InitAdapter(item, propId);
                return adapter;
            }
            catch (Exception)
            {
                return new CT();
            }
            
        }

        public void IncludeRawValues(Item dataItem)
        {
            var viewModelType = typeof(T);

            foreach (var pi in viewModelType.GetProperties())
            {
                var propertyIdentifier = $"{viewModelType.FullName}.{pi.Name}";
                ID sitecoreFieldId;
                if (MappingTable.Instance.IncludeMap.TryGetValue(propertyIdentifier, out sitecoreFieldId))
                {
                    pi.SetValue(this, ParseValue(pi.PropertyType, dataItem.Fields[sitecoreFieldId]));
                }
            }
        }
        private object ParseValue(Type propertyType, Field field)
        {
            int intValue;
            bool boolValue;
            Type viewModelType;
            var value = field.Value;
            if (propertyType == typeof(int) && int.TryParse(value, out intValue))
                return intValue;
            if (propertyType == typeof(bool))
                return value == "1";
            if (propertyType == typeof(DateTime))
            {
                var dateField = (DateField)field;
                if(dateField != null)
                    return dateField.DateTime;
            } 
            if (MappingTable.Instance.ViewModelRegistry.TryGetValue(propertyType.FullName, out viewModelType))
            {
                ReferenceField refItem = field;
                if (refItem == null || refItem.TargetItem == null)
                    return null;
                var viewModel = (IPropellerModel) Activator.CreateInstance(propertyType);
                viewModel.DataItem = refItem.TargetItem;
                viewModel.IncludeRawValues(refItem.TargetItem);

                return viewModel;
            }

            if (propertyType == typeof(string))
                return value;

            Log.Warn($"MvcViewModel: Not supported value type '{propertyType.FullName}'", this);
            return null;
        }

        public bool Commit(string username)
        {

            var dataItem = DataItem;

            if (dataItem == null)
                return false;

            var viewModelType = typeof(T);

            foreach (var pi in viewModelType.GetProperties())
            {
                var propertyIdentifier = $"{viewModelType.FullName}.{pi.Name}";
                ID sitecoreFieldId;
                if (MappingTable.Instance.EditableMap.TryGetValue(propertyIdentifier, out sitecoreFieldId))
                {
                    var value = pi.GetValue(this) as string;
                    if (value == null)
                    {
                        Log.Warn($"Failed to edit property '{propertyIdentifier}', value is null", this);
                        continue;
                    }

                    using (new Sitecore.Security.Accounts.UserSwitcher(username, true))
                    {
                        dataItem.Editing.BeginEdit();
                        try
                        {
                            dataItem.Fields[sitecoreFieldId].Value = value;
                        }
                        finally
                        {
                            //Close the editing state
                            dataItem.Editing.EndEdit();
                        }
                    }


                }
            }


            return true;
        }

        
    }
}