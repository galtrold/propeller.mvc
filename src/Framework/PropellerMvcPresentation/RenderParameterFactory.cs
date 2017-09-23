using System;
using System.CodeDom;

namespace Propeller.Mvc.Presentation
{
    public class RenderParameterFactory
    {
        public T1 ValueTranformation<T1>(string value)
        {
            var typeName = typeof(T1).FullName;

            try
            {
                if (typeName == typeof(bool).FullName)
                    return (T1)Convert.ChangeType(value, typeof(bool));
                if (typeName == typeof(int).FullName)
                    return (T1)Convert.ChangeType(int.Parse(value), typeof(int));
                if (typeName == typeof(double).FullName)
                    return (T1)Convert.ChangeType(double.Parse(value), typeof(double));
                if (typeName == typeof(float).FullName)
                    return (T1)Convert.ChangeType(float.Parse(value), typeof(float));

                return default(T1);
            }
            catch (Exception e)
            {
                return default(T1);
            }

        }
    }
}