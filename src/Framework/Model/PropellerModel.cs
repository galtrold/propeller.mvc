using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Propeller.Mvc.Core;
using Propeller.Mvc.Core.Processing;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Propeller.Mvc.Model
{
    public class PropellerModel<T> : PropellerEntity<T>, IPropellerModel
    {


        public PropellerModel(Item dataItem) : base(dataItem)
        {
            if (dataItem != null)
                IncludeRawValues(dataItem);
        }


        public TP GetItemReference<TP>(Expression<Func<T, object>> expression) where TP : PropellerEntity<TP>, new()
        {

            if (Item == null)
                return new TP();

            var propId = GetPropertyId(expression);
            ReferenceField dropDownSelectedItem = Item.Fields[propId];
            if (dropDownSelectedItem == null)
            {
                return new TP();
            }

            var targetItem = dropDownSelectedItem.TargetItem;
            return new TP { DataItem = targetItem };
        }

        public IEnumerable<TK> GetList<TK>(Expression<Func<T, object>> expression) where TK : PropellerEntity<TK>, new()
        {

            if (Item == null)
                return new List<TK>();

            var propId = GetPropertyId(expression);

            var listField = Item.Fields[propId];

            var database = Sitecore.Context.Database;

            var listItemIds = listField.Value.Split('|');

            if (string.IsNullOrWhiteSpace(listField.Value) || listItemIds.Length < 1)
                return new List<TK>();

            var listItems = listItemIds.Select(p => new TK { DataItem = database.GetItem(p) });

            return listItems;
        }

        public CT GetAs<CT>(Expression<Func<T, object>> expression) where CT : new()
        {
            return new CT();
        }

        public virtual Item GetDataItem()
        {
            return this.DataItem;
        }

        private void IncludeRawValues(Item dataItem)
        {
            var viewModelType = typeof(T);

            foreach (var pi in viewModelType.GetProperties())
            {
                var propertyIdentifier = $"{viewModelType.FullName}.{pi.Name}";
                ID sitecoreFieldId;
                if (MappingTable.Instance.IncludeMap.TryGetValue(propertyIdentifier, out sitecoreFieldId))
                {
                    pi.SetValue(this, this.ParseValue(pi.PropertyType, dataItem.Fields[sitecoreFieldId].Value));
                }
            }
        }
        private object ParseValue(Type propertyType, string value)
        {
            int intValue;
            bool boolValue;
            DateTime dateValue;
            if (propertyType == typeof(int) && int.TryParse(value, out intValue))
                return intValue;
            if (propertyType == typeof(bool) && bool.TryParse(value, out boolValue))
                return boolValue;
            if (propertyType == typeof(string))
                return value;
            if (propertyType == typeof(DateTime) && DateTime.TryParse(value, out dateValue))
                return dateValue;

            Log.Warn($"MvcViewModel: Not supported value type '{propertyType.FullName}'", this);
            return null;
        }

        public bool Commit()
        {

            var dataItem = GetDataItem();

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

                    using (new Sitecore.SecurityModel.SecurityDisabler())
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