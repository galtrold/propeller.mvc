using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Propeller.Mvc.Core.Processing;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;
using Sitecore.StringExtensions;

namespace Propeller.Mvc.Core
{
    public abstract class PropellerEntity<T> 
    {
        protected Item _dataItem;

        public virtual Item DataItem
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

        public virtual Item Item { get; set; }

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


    }
}