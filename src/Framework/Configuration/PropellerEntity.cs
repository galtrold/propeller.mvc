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
        public Item DataItem;

        [JsonIgnore]
        public string ItemId => Item?.ID.ToString() ?? "";

        [JsonIgnore]
        public string ItemUrl => Item != null ? LinkManager.GetItemUrl(Item).Replace(" ", "%20") : "WARN-dataitem-not-set";

        [JsonIgnore]
        public string ItemName => Item != null ? Item.Name : "WARN-dataitem-not-set";

        [JsonIgnore]
        public string ItemDisplayName => Item != null ? Item.DisplayName : "WARN-dataitem-not-set";

        protected PropellerEntity(Item dataItem)
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

        public virtual Item Item
        {
            get { return this.DataItem; }
            set { DataItem = value; }
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


    }
}