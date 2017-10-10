using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Propeller.Mvc.Core;
using Propeller.Mvc.Core.Processing;
using Propeller.Mvc.Model.Adapters;
using Propeller.Mvc.Model.Factory;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;


namespace Propeller.Mvc.Model
{
    public class PropellerModel<T> : PropellerEntity<T>, IPropellerModel where T : IPropellerModel, new()
    {

        public bool Commit(string username)
        {

            var dataItem = DataItem;

            if (dataItem == null)
                return false;

            var viewModelType = typeof(T);

            foreach (var pi in viewModelType.GetProperties())
            {
                var propertyIdentifier = $"{viewModelType.FullName}.{pi.Name}";
                ID sitecoreFieldId;
                if (MappingTable.Instance.EditableMap.TryGetValue(propertyIdentifier, out sitecoreFieldId))
                {
                    var value = pi.GetValue(this) as string;
                    if (value == null)
                    {
                        Log.Warn($"Failed to edit property '{propertyIdentifier}', value is null", this);
                        continue;
                    }

                    using (new Sitecore.Security.Accounts.UserSwitcher(username, true))
                    {
                        dataItem.Editing.BeginEdit();
                        try
                        {
                            dataItem.Fields[sitecoreFieldId].Value = value;
                        }
                        finally
                        {
                            //Close the editing state
                            dataItem.Editing.EndEdit();
                        }
                    }


                }
            }


            return true;
        }

        
    }
}