using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Propeller.Mvc.View
{
    public static class TemplateRender
    {
        public static MvcHtmlString Template<TModel, TValue>(this IPropellerTemplate<TModel> vm, Expression<Func<TModel, TValue>> expression)
        {
            if (Sitecore.Context.PageMode.IsExperienceEditor)
            {
                var model = vm.GetDataItem();

                var templateHelperMethod = expression.Body as System.Linq.Expressions.MethodCallExpression;

                var propertyField = templateHelperMethod.Arguments[0] as System.Linq.Expressions.MemberExpression;

                var propName = "Not found yet";
                if (propertyField != null)
                    propName = propertyField.Member.Name;

                return new MvcHtmlString(propName);


            }
            else
            {
                var result = expression.Compile();
                var value = result( vm.TemplateArg);
                var r = new MvcHtmlString(value.ToString());
                return r;
            }
        }
    }
}