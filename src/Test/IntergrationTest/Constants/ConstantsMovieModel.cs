using Sitecore.Data;

namespace IntergrationTest.Constants
{
    public class ConstantsMovieModel
    {
        public struct Instances
        {
            public static ID ANewHope = ID.NewID;
            public static ID EmpireStrikesBack = ID.NewID;

        }

        public struct Templates
        {
            public static ID MovieTemplateId = ID.NewID;
        }
        public struct Fields
        {
            public const string TitleField = "{E6245D48-0D6A-49B0-9DA2-412145F71D83}";
            public const string ReleaseDateField = "{80D5A5C9-26FE-4FF6-AE3C-F9AE229E2958}";
        }
    }
}