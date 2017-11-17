using Sitecore.Data;

namespace IntergrationTest.Constants
{
    public class ConstantsCpuModel
    {
        public struct Instances
        {
            public static ID Hasswell = ID.NewID;
            public static ID IvyBridge = ID.NewID;

        }

        public struct Templates
        {
            public static ID CpuTemplateId = ID.NewID;
        }
        public struct Fields
        {
            public static ID ArchitectureNameField = ID.NewID;
            public static ID Predecessor = ID.NewID;
            public static ID Successor = ID.NewID;
        }
    }
}