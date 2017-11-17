using System;
using System.Linq.Expressions;
using System.Web;
using Newtonsoft.Json;
using Propeller.Mvc.Model;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Presentation;
using Sitecore.StringExtensions;
using Sitecore.Web.UI.WebControls;
using Rendering = Sitecore.Mvc.Presentation.Rendering;
using Sitecore.Data.Fields;
using System.Collections.Generic;
using Propeller.Mvc.Model.Adapters;
using Propeller.Mvc.Model.ItemTools;

namespace Propeller.Mvc.Presentation
{
    public class PropellerViewModel<T> : PropellerModel<T> where T : IPropellerModel, new()
    {
        public HtmlString Render(Expression<Func<T, object>> expression)
        {
            var fieldId = "";
            var itemId = "";
            try
            {
                var propId = GetPropertyId(expression);

                if(propId == ID.Null)
                    return new HtmlString(string.Empty);

                fieldId = propId.ToString();

                var item = DataItem;


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

        public TP GetItemReference<TP>(Expression<Func<T, object>> expression) where TP : PropellerModel<TP>, new()
        {
            var itemRepository = new ItemRepository();
            var type = typeof(TP);
            
            var item = DataItem;

            if (item == null)
                return Activator.CreateInstance(type) as TP;

            var propId = GetPropertyId(expression);
            if (propId == ID.Null)
                return Activator.CreateInstance(type) as TP;

            var targetItem = itemRepository.GetReferencedItem(item, propId);

            var vm = Activator.CreateInstance(type) as TP;
            vm.DataItem = targetItem;
            return vm;
        }

        public CT GetAs<CT>(Expression<Func<T, object>> expression) where CT : IFieldAdapter, new()
        {
            try
            {
                var item = DataItem;
                var propId = GetPropertyId(expression);
                if (propId == ID.Null)
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

        public IList<TK> GetList<TK>(Expression<Func<T, object>> expression) where TK : PropellerModel<TK>, new()
        {
            var itemRepository = new ItemRepository();
            if (DataItem == null)
                return new List<TK>();

            var propId = GetPropertyId(expression);
            if (propId == ID.Null)
                return new List<TK>();

            var itemList = itemRepository.GetItemList(DataItem, propId);
            var type = typeof(TK);
            var results = new List<TK>();
            foreach (var item in itemList)
            {
                var viewModelItem = (TK)Activator.CreateInstance(type);
                viewModelItem.DataItem = item;
                results.Add(viewModelItem);
            }

            return results;
        }

    }
}