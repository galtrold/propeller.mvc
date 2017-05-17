using Sitecore.Data.Events;

namespace Propeller.Mvc.Presentation
{
    public class SCPageMode
    {
        private static bool _hasExperienceEditor;

        static SCPageMode()
        {
            var t = typeof(Sitecore.Context.PageMode);
            _hasExperienceEditor = t.GetProperty("IsExperienceEditor") != null;
        }

        public static bool IsEditMode()
        {
            if(_hasExperienceEditor)
                return Sitecore.Context.PageMode.IsExperienceEditor;
            return Sitecore.Context.PageMode.IsPageEditor;

        }
    }
}