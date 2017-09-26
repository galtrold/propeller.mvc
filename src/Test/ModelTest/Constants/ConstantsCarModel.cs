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
            public static readonly ID CarPhoto = new ID("{1B8E4A74-20FC-4708-8CEF-5D7C2D38A5B2}");
            public static readonly ID EnteredProductionDateField = new ID("{DC6C2B78-F016-451D-9BE5-0B604A3D7BBB}");
            public static readonly ID ManuFactureField = new ID("{30F116C5-BF0B-49E7-8615-8BF9BF2AA2DD}");
            public static readonly ID IsTurboCharged = new ID("{75BD0CD1-CB60-46E8-94E3-7ECFD374B729}");
            public static readonly ID CarModelField = new ID(   "{A693770F-4964-4593-A189-0D25443CBF1D}");
            public static readonly ID CarClassField = new ID("{988B759F-5234-4E18-AA63-2281BDC07029}");
            public static readonly ID ExternalWikiLink = new ID("{F8DA8031-83E8-4547-B81B-FC6E6342946C}");
            public static readonly ID ProductionCountry = new ID("{F8DA8031-83E8-4547-B81B-FC6E6342946D}");
            public static readonly ID MediaImageItem = new ID("{5EA35D30-EF21-4217-AFE8-D71F5867DCC7}");

        }
    }

    public class ConstantsCountryModel
    {
        public struct LinkItems
        {
            public static ID CountryItem = ID.NewID;

        }

        public struct Templates
        {
            public static ID CountryTemplateId = ID.NewID;
        }
        public struct Fields
        {
            public static readonly ID NameField = new ID("{E6245D48-0D6A-49B0-9DA2-412145F71D83}");
            public static readonly ID CurrencyField = new ID("{80D5A5C9-26FE-4FF6-AE3C-F9AE229E2958}");
            public static readonly ID PetrolTaxField = new ID("{EA83D3D0-BC05-4EE9-B22B-6B70F387C743}");
            public static readonly ID DieselTaxField = new ID("{C1611192-5A00-4D04-BE02-494DB77BB0C8}");
        }
    }
}