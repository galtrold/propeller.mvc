using Propeller.Mvc.Model;

namespace Propeller.Mvc.View
{
    public interface IPropellerTemplate<T> : IPropellerModel
    {
        T TemplateArg();


    }
}