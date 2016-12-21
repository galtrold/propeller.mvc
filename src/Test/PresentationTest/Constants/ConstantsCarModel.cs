using System;
using Sitecore.Data;

namespace PresentationTest.Constants
{
    public class ConstantsCarModel
    {
        public struct Templates
        {
            public static ID CarTemplateId = ID.NewID;
        }
        public struct Fields
        {
            public const string ManuFactureField = "{30F116C5-BF0B-49E7-8615-8BF9BF2AA2DD}";
            public const string CarModelField =    "{A693770F-4964-4593-A189-0D25443CBF1D}";
            public const string CarClassField = "{988B759F-5234-4E18-AA63-2281BDC07029}";
        }
    }
}