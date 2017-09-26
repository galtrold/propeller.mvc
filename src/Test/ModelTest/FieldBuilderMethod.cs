using NSubstitute;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace ModelTest
{
    public static  class FieldBuilderMethod
    {
        public static FieldCollection AddField(this FieldCollection collection, ID id, string value, Item owner)
        {
            var field = Substitute.For<Field>(id, owner);
            field.Value.Returns(value);

            collection[id].Returns(field);
            return collection;
        }
    }
}