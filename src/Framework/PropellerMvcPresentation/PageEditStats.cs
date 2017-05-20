using Sitecore;

namespace Propeller.Mvc.Presentation
{
    public class PageEditStats
    {
        public static bool IsPageEditor =>
           IsExperienceEditor;

        public static bool IsPageEditorEditing =>
            IsExperienceEditorEditing;

        public static bool IsExperienceEditor =>
             GetPropertyValue(DoesPropertyExist("IsExperienceEditor") ? "IsExperienceEditor" : "IsPageEditor");

        public static bool IsExperienceEditorEditing =>
            GetPropertyValue(DoesPropertyExist("IsExperienceEditorEditing") ? "IsExperienceEditorEditing" : "IsPageEditorEditing");

        private static bool DoesPropertyExist(string propertyName)
        {
            return (typeof(Context.PageMode).GetProperty(propertyName) != null);
        }

        private static bool GetPropertyValue(string propertyName)
        {
            var prop = typeof(Context.PageMode).GetProperty(propertyName);
            return (bool)(prop.GetValue(typeof(Context.PageMode), null));
        }

    }
}