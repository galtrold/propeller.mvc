using Sitecore.Data;

namespace IntergrationTest.Constants
{
    public class ConstantVehicleClassModel
    {
        public struct Instances
        {
            public static ID Fighter = ID.NewID;

        }

        public struct Templates
        {
            public static ID VehicleClassTemplateId = ID.NewID;
        }
        public struct Fields
        {
            public const string NameField = "{E6245D48-0D6A-49B0-9DA2-412145F71D83}";
        }
    }
}