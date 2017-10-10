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
            public const string ClassNameField = "{EE7BBF3A-9558-4F26-9DA9-D335769FA09E}";
        }
    }
}