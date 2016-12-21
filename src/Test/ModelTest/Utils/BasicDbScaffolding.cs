using ModelTest.Constants;
using Sitecore.Data;
using Sitecore.FakeDb;

namespace ModelTest.Utils
{
    public class BasicDbScaffolding
    {
        public static Db SetupDatebaseWithSimpleFieldTypeAndValue(string fieldId, string value)
        {
            var db = new Db()
            {
                new DbTemplate("car", ConstantsCarModel.Templates.CarTemplateId)
                {
                    {new ID(fieldId), value },
                },
                new DbItem("Ford500", ID.NewID, ConstantsCarModel.Templates.CarTemplateId)
            };
            return db;
        }
    }
}