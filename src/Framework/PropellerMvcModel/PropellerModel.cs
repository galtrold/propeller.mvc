using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Propeller.Mvc.Core;
using Propeller.Mvc.Core.Processing;
using Sitecore.Data;
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
                    string value = null;
                    if (typeof(IEnumerable<IPropellerModel>).IsAssignableFrom(pi.PropertyType))
                    {
                        if (pi.GetValue(this) is IEnumerable<IPropellerModel> list)
                        {
                            StringBuilder stringBuilder = new StringBuilder();
                            foreach (var propellerModel in list)
                            {
                                if (stringBuilder.Length > 0)
                                    stringBuilder.Append('|');
                                stringBuilder.Append(propellerModel.DataItem.ID);
                            }

                            value = stringBuilder.ToString();
                        }
                    }
                    else
                    {
                        value = pi.GetValue(this) as string;
                    }


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

        /// <summary>
        /// If the model need some initialization post creation time you can override the method.
        /// </summary>

        public virtual void Init()
        {
            
        }
    }
}