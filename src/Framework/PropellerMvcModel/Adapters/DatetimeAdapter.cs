using System;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Propeller.Mvc.Model.Adapters
{
    public class DateTimeAdapter : IFieldAdapter
    {
        public DateTime DateTimeValue { get; set; }

        public void InitAdapter(Item item, ID propId)
        {
            var field = item.Fields[propId];

            var dateField = (DateField)field;
            if (dateField != null)
                DateTimeValue = dateField.DateTime;

        }
    }
}