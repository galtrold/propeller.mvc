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
        
    }
}