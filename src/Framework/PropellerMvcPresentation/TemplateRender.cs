using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using Propeller.Mvc.Core.Processing;
using Sitecore.Data;
using Sitecore.Web.UI.WebControls;

namespace Propeller.Mvc.Presentation
{
    public static class TemplateRender
    {

        public static MvcHtmlString Template<TModel, TValue>(this IPropellerTemplate<TModel> vm, Expression<Func<TModel, TValue>> expression)
        {
            //var sitecoreAssembly = Assembly.LoadFile("Sitecore.Kernal.dll");

            //var pageMode = sitecoreAssembly.GetType("Sitecore.Context.PageMode");

            //pageMode.InvokeMember("IsExperience")


            if (SCPageMode.IsEditMode())
            {
                var expressionBody = expression.Body as System.Linq.Expressions.MethodCallExpression;
                try
                {
                    var getAsMethod = expressionBody.Arguments.FirstOrDefault() as MethodCallExpression;
                    var unaryExpression = getAsMethod.Arguments.FirstOrDefault() as UnaryExpression;
                    var innerExpresion = unaryExpression.Operand as LambdaExpression;
                    var propertyField = innerExpresion.Body as System.Linq.Expressions.MemberExpression;

                    var propName = propertyField.Member.Name;

                    var fullyQualifiedName = typeof(TModel).FullName;

                    var key = $"{fullyQualifiedName}.{propName}";

                    Func<ID> idFunc;
                    if (MappingTable.Instance.JumpMap.TryGetValue(key, out idFunc))
                    {
                        var fieldId = idFunc();

                        var htmlStr = FieldRenderer.Render(vm.DataItem, fieldId.ToString());
                        return new MvcHtmlString(htmlStr);
                    }



                    return new MvcHtmlString("sd");
                }
                catch (Exception)
                {
                    
                    throw;
                }
                

            }
            else
            {
                var result = expression.Compile();
                var value = result( vm.TemplateArg());
                var r = new MvcHtmlString(value.ToString());
                return r;
            }
        }
    }
}