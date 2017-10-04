using Sitecore.Data;

namespace IntergrationTest.Constants
{
    public class ConstantsVehicleModel
    {
        public struct Instances
        {
            public static ID XWing = ID.NewID;
        }
        public struct Templates
        {
            public static ID VehiclTemplateId = ID.NewID;
        }
        public struct Fields
        {
            public const string PhotoField = "{1B8E4A74-20FC-4708-8CEF-5D7C2D38A5B2}";
            public const string AppearanceField = "{DC6C2B78-F016-451D-9BE5-0B604A3D7BBB}";
            public const string NameField = "{30F116C5-BF0B-49E7-8615-8BF9BF2AA2DD}";
            public const string HasHyperdrive = "{75BD0CD1-CB60-46E8-94E3-7ECFD374B729}";
            public const string CargoField =    "{A693770F-4964-4593-A189-0D25443CBF1D}";
            public const string LengthField = "{988B759F-5234-4E18-AA63-2281BDC07029}";
            public const string ExternalWikiLink = "{F8DA8031-83E8-4547-B81B-FC6E6342946C}";
            public const string Class = "{F8DA8031-83E8-4547-B81B-FC6E6342946D}";
            public const string MediaImageItem = "{5EA35D30-EF21-4217-AFE8-D71F5867DCC7}";

        }
    }
}