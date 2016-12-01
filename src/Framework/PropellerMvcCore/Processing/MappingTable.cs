using System;
using System.Collections.Generic;
using Sitecore.Data;

namespace Propeller.Mvc.Core.Processing
{
    /// <summary>
    /// Global dictionaries which stores Property names (fully qualified names) with Sitecore Field Ids
    /// </summary>
    public class MappingTable
    {
        private static volatile MappingTable _instance;
        private static readonly object SyncRoot = new object();



        private MappingTable()
        {
        }

        public static MappingTable Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                lock (SyncRoot)
                {
                    if (_instance != null)
                        return _instance;
                    _instance = new MappingTable
                    {
                        Map = new Dictionary<string, ID>(),
                        IncludeMap = new Dictionary<string, ID>(),
                        EditableMap = new Dictionary<string, ID>(),
                        JumpMap = new Dictionary<string, Func<ID>>(),
                        ViewModelRegistry =  new Dictionary<string, Type>()
                       
                    };
                }
                return _instance;
            }
        }

        

        public Dictionary<string, ID> Map { get; set; }
        public Dictionary<string, ID> IncludeMap { get; set; }
        public Dictionary<string, ID> EditableMap { get; set; }

        public Dictionary<string, Func<ID>> JumpMap { get; set; }
        public Dictionary<string, Type> ViewModelRegistry { get; set; }
    }
}