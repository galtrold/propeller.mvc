using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Newtonsoft.Json;
using Propeller.Mvc.Core.Processing;
using Propeller.Mvc.Model;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;
using Sitecore.StringExtensions;
using Sitecore.Web.UI.WebControls;
using Rendering = Sitecore.Mvc.Presentation.Rendering;

namespace Propeller.Mvc.View
{
    public abstract class PropellerViewModel<T> : PropellerModel<T>, IRenderingModel 
    {
        private Rendering _rendering;

        public override Item Item
        {
            get { return _rendering.Item; } 
        }

        public virtual Item PageItem
        {
            get
            {
                return PageContext.Current.Item;
            }
        }
        
        public virtual Rendering Rendering
        {
            get
            {
                if (this._rendering == null)
                    throw new InvalidOperationException("{0} has not been initialized.".FormatWith((object)this.GetType()));
                return this._rendering;
            }
            set
            {
                this._rendering = value;
            }
        }

        public virtual T Self { get; set; }

        public PropellerViewModel(Item dataItem) : base(dataItem)
        {
        }

        public PropellerViewModel(Rendering rendering) : base(rendering.Item) 
        {
            Initialize(rendering);
        }

        public void Initialize(Rendering rendering)
        {
            _rendering = rendering;
        }

        public override Item GetDataItem()
        {
            if (Item != null)
                return Item;

            if (PageItem != null)
                return PageItem;

            if (DataItem != null)
                return DataItem;

            Log.Error("Error now data item available", this);
            return null;

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
        
    }
}