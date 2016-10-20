using System;
using System.Linq.Expressions;

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
                    return expression.Body as MemberExpression;

                if (expression.Body is UnaryExpression)
                    return ((UnaryExpression)expression.Body).Operand as MemberExpression;

                return null;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}