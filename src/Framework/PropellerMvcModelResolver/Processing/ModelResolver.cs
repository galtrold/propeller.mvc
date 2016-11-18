
using System;
using System.Web.Compilation;
using Sitecore.Mvc.Pipelines.Response.GetModel;
using Sitecore.Mvc.Presentation;


namespace Propeller.Mvc.ModelResolver.Processing
{

    /// <summary>
    /// The ModelResolver is only used otgether with Sitecore ViewRenderings.
    /// ModelResolver will resolve the data model specified in the View file, instanciate the correct model type and pass it on to the Sitecore context model.
    /// </summary>
    public class ModelResolver : GetModelProcessor
    {
        public override void Process(GetModelArgs args)
        {
            if (args.Result != null)
                return;

            if (args.Rendering.RenderingType != "Layout" && args.Rendering.RenderingType != "View" && args.Rendering.RenderingType != "r" && args.Rendering.RenderingType != "d")
                return;

            var viewRenderer = args.Rendering.Renderer as ViewRenderer;
            if (viewRenderer == null)
                return;

            var compiledViewType = BuildManager.GetCompiledType(viewRenderer.ViewPath);
            var baseType = compiledViewType.BaseType;

            if (baseType == null || !baseType.IsGenericType)
                return;

            var modelType = baseType.GetGenericArguments()[0];

            if (modelType == typeof(object))
                return;

            args.Result = Activator.CreateInstance(modelType);
        }
    }
}