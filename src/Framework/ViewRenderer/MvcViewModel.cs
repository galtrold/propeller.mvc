using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Newtonsoft.Json;
using Propeller.Mvc.Configuration.Processing;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;
using Rendering = Sitecore.Mvc.Presentation.Rendering;

namespace Propeller.Mvc.ViewRenderer
{
    public class MvcViewModel<T> : RenderingModel
    {

        [JsonIgnore]
        public string ItemId => Item?.ID.ToString() ?? "";

        [JsonIgnore]
        public string ItemUrl => Item != null ? LinkManager.GetItemUrl(Item).Replace(" ", "%20") : "WARN-dataitem-not-set";

        [JsonIgnore]
        public string ItemName => Item != null ? Item.Name : "WARN-dataitem-not-set";

        [JsonIgnore]
        public string ItemDisplayName => Item != null ? Item.DisplayName : "WARN-dataitem-not-set";

        [JsonIgnore]
        private readonly Item _dataItem;

        [JsonIgnore]
        public override Item Item => Rendering.Item ?? _dataItem;

        public MvcViewModel()
        {

        }

        public MvcViewModel(Item dataItem)
        {
            _dataItem = dataItem;

        }

        public MvcViewModel(Rendering rendering)
        {

        }
        
        public override void Initialize(Rendering rendering)
        {
            if (rendering != null)
                base.Initialize(rendering);

            // Include raw values from Sitecore. This will only happen if the ViewModel has been configured to include raw values.
            var dataItem = GetDataItem();
            if (dataItem != null)
                IncludeRawValues(dataItem);
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

        public HtmlString Render(Expression<Func<T, object>> expression)
        {
            var fieldId = "";
            var itemId = "";
            try
            {
                var propId = GetPropertyId(expression);
                fieldId = propId.ToString();

                var item = GetDataItem();


                itemId = item.ID.ToString();
                var field = FieldRenderer.Render(item, fieldId);
                if (field != null)
                {
                    return new HtmlString(field);
                }
                return new HtmlString(string.Empty);

            }
            catch (Exception ex)
            {

                Log.Error($"An error occured while rendering a field ({fieldId}) on dataItem ({itemId}). Rich text fields can sometimes contaion invalid html. Try formatting it in the sitecore editor", ex, this);
                return new HtmlString("Ups.. someone droped the letters during the typing of this field. We will have this mess cleaned up as soon a possible.");

            }

        }

        public TP GetItemReference<TP>(Expression<Func<T, object>> expression) where TP : MvcViewModel<TP>, new()
        {
            if (DataItem == null)
                return new TP();

            var propId = GetPropertyId(expression);
            ReferenceField dropDownSelectedItem = DataItem.Fields[propId];
            if (dropDownSelectedItem == null)
            {
                return new TP();
            }

            var targetItem = dropDownSelectedItem.TargetItem;
            return new TP { DataItem = targetItem };
        }

        public IEnumerable<TK> GetList<TK>(Expression<Func<T, object>> expression) where TK : MvcViewModel<TK>, new()
        {

            var item = GetDataItem();

            if (item == null)
                return new List<TK>();

            var propId = GetPropertyId(expression);

            var listField = item.Fields[propId];

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

        protected ID GetPropertyId(Expression<Func<T, object>> expression)
        {

            MemberExpression memberExpression = GetMemberExpression(expression);


            var propName = memberExpression?.Member.Name;

            if (string.IsNullOrEmpty(propName))
                return new ID("");

            var fullyQualifiedName = typeof(T).FullName;

            var key = $"{fullyQualifiedName}.{propName}";


            Func<ID> idFunc;
            if (MappingTable.Instance.JumpMap.TryGetValue(key, out idFunc))
                return idFunc();

            Log.Error($"Kunne ikke finde item id for '{key}'", this);
            return new ID("");
        }

        private MemberExpression GetMemberExpression(Expression<Func<T, object>> expression)
        {
            try
            {
                if (expression.Body is MemberExpression)
                    return expression.Body as MemberExpression;

                if (expression.Body is UnaryExpression)
                    return ((UnaryExpression)expression.Body).Operand as MemberExpression;

                return null;

            }
            catch (Exception ex)
            {
                Log.Error("Model expression failes to resolve MemberExpression", ex, this);
                return null;
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

        private Item GetDataItem()
        {
            return Item ?? PageItem;
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
    }
}