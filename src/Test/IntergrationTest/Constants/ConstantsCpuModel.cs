using Sitecore.Data;

namespace IntergrationTest.Constants
{
    public class ConstantsCpuModel
    {
        public struct Instances
        {
            public static ID Hasswell = new ID("{727E0BCC-99AD-4E5F-BFF9-3587801A0935}");
            public static ID IvyBridge = new ID("{13941178-4102-46C9-9E02-B5256F7E02CC}");

        }

        public struct Templates
        {
            public static ID CpuTemplateId = new ID("{529C51DC-476B-447D-8C24-62A5E6EFF229}");
        }
        public struct Fields
        {
            public static ID ArchitectureNameField = new ID("{7A3CC4FF-6803-49F5-A6D1-5C8EA2377705}");
            public static ID Predecessor = new ID("{72180B9C-255D-4064-8FFA-4F3DA2B7BBC7}");
            public static ID Successor = new ID("{5195C739-ACE2-469B-AF75-2D5D0A29A76B}");
        }
    }
}