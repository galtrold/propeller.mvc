﻿using System;
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

namespace Propeller.Mvc.Presentation
{
    public class PropellerViewModel<T> : PropellerModel<T>, IRenderingModel where T : IPropellerModel, new()
    {
        [JsonIgnore]
        private Rendering _rendering;

        [JsonIgnore]
        public virtual Item PageItem
        {
            get
            {
                return PageContext.Current.Item;
            }
        }

        [JsonIgnore]
        public virtual Rendering Rendering
        {
            get
            {
                if (_rendering == null)
                    throw new InvalidOperationException("{0} has not been initialized.".FormatWith((object)GetType()));
                return _rendering;
            }
            set
            {
                _rendering = value;
            }
        }
        
        public PropellerViewModel() { }

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

        [JsonIgnore]
        public override Item DataItem
        {
            get
            {
                if (_rendering != null )
                    return _rendering.Item;

                if (_dataItem != null)
                    return _dataItem;

                if (PageItem != null)
                    return PageItem;

                Log.Error("Error now data item available", this);
                return null;
            }
        }

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
        
    }
}