using Propeller.Mvc.Model;

namespace Propeller.Mvc.Presentation
{
    public interface IPropellerTemplate<T> : IPropellerModel
    {
        T TemplateArg();


    }
}