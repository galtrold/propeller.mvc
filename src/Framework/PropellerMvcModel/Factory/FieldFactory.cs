using System;
using System.Linq;
using Propeller.Mvc.Model.Adapters;
using Propeller.Mvc.Model.Strategies;

namespace Propeller.Mvc.Model.Factory
{
    internal class FieldFactory
    {
        public IFieldStrategy GetFieldStrategy(Type propertyType)
        {

            if (propertyType == typeof(string))
                return new StringFieldStrategy();

            if (propertyType == typeof(int))
                return new IntegerFieldStrategy();

            if (propertyType == typeof(bool))
                return new BooleanFieldStrategy();

            if (propertyType == typeof(double) || propertyType == typeof(float) || propertyType == typeof(decimal))
                return new FloatingFieldStrategy();

            if (propertyType == typeof(DateTime))
                return new DateFieldStrategy();

            if (propertyType.GetInterfaces().Contains(typeof(IFieldAdapter)))
                return new AdapterFieldStrategy();

                return new EmptyFieldStrategy();
        }
    }
}