using System;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Propeller.Mvc.Core.Processing;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;

namespace Propeller.Mvc.Core
{
    public class PropellerEntity<T>
    {
        [JsonIgnore]
        protected Item _dataItem;


        [JsonIgnore]
        public Item DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        [JsonIgnore]
        public string Id => DataItem?.ID.ToString() ?? "";

        [JsonIgnore]
        public string Url => DataItem != null ? LinkManager.GetItemUrl(DataItem).Replace(" ", "%20") : "WARN-dataitem-not-set";

        [JsonIgnore]
        public string ItemName => DataItem != null ? DataItem.Name : "WARN-dataitem-not-set";

        [JsonIgnore]
        public string DisplayName => DataItem != null ? DataItem.DisplayName : "WARN-dataitem-not-set";

        public PropellerEntity() { }

        public PropellerEntity(Item dataItem)
        {
            if(dataItem == null)
                Log.Warn($"Instanciating propeller entity of type '{this.GetType().FullName}' with no data item. Is null", this);
            DataItem = dataItem;

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


        protected ID GetPropertyId(Expression<Func<T, object>> expression)
        {

            MemberExpression memberExpression = GetMemberExpression(expression);


            var propName = memberExpression?.Member.Name;

            if (string.IsNullOrEmpty(propName))
                return ID.Null;

            return GetPropertyIdByName(propName);

        }

        protected ID GetPropertyIdByName(string propName)
        {
            var fullyQualifiedName = typeof(T).FullName;

            var key = $"{fullyQualifiedName}.{propName}";


            Func<ID> idFunc;
            if (MappingTable.Instance.JumpMap.TryGetValue(key, out idFunc))
                return idFunc();

            Log.Error($"Kunne ikke finde item id for '{key}'", this);
            return ID.Null;
        }


    }
}