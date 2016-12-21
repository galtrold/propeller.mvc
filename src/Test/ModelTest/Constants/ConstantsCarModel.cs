using Sitecore.Data;

namespace ModelTest.Constants
{
    public class ConstantsCarModel
    {
        public struct Templates
        {
            public static ID CarTemplateId = ID.NewID;
        }
        public struct Fields
        {
            public const string CarPhoto = "{1B8E4A74-20FC-4708-8CEF-5D7C2D38A5B2}";
            public const string EnteredProductionDateField = "{DC6C2B78-F016-451D-9BE5-0B604A3D7BBB}";
            public const string ManuFactureField = "{30F116C5-BF0B-49E7-8615-8BF9BF2AA2DD}";
            public const string IsActive = "{75BD0CD1-CB60-46E8-94E3-7ECFD374B729}";
            public const string CarModelField =    "{A693770F-4964-4593-A189-0D25443CBF1D}";
            public const string CarClassField = "{988B759F-5234-4E18-AA63-2281BDC07029}";
            public const string ExternalWikiLink = "{F8DA8031-83E8-4547-B81B-FC6E6342946C}";
        }
    }
}