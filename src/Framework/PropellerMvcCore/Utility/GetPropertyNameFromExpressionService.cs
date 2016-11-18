using System;
using System.Linq.Expressions;
using Sitecore.Diagnostics;

namespace Propeller.Mvc.Core.Utility
{
    /// <summary>
    /// Utility class used to grab MemberExpressions
    /// </summary>
    internal class GetPropertyNameFromExpressionService
    {
        internal static string Get<T>(Expression<Func<T, object>> expression)
        {
            var body = GetMemberExpression(expression);
            return body?.Member.Name ?? string.Empty;
        }

        private static MemberExpression GetMemberExpression<T>(Expression<Func<T, object>> expression)
        {
            try
            {
                if (expression.Body is MemberExpression)
                    return (MemberExpression) expression.Body;

                if (expression.Body is UnaryExpression)
                    return ((UnaryExpression)expression.Body).Operand as MemberExpression;

                return null;

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, new object());
                return null;
            }
        }
    }
}