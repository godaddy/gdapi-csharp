using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json.Linq;

namespace gdapi
{
    public class SchemaResource : Resource
    {

        public Dictionary<string, Dictionary<string, string>> actions { get; set; }
        public string[] collectionMethods { get; set; }
        public Dictionary<string, string> collectionActions { get; set; }
        public Dictionary<string, Dictionary<string,JToken>> collectionFilters { get; set;}
        public Dictionary<string, Dictionary<string, JToken>> fields { get; set; }
        public string[] methods { get; set; }


        /// <summary>
        /// Constructs a new Resource
        /// </summary>
        public SchemaResource()
        {
            this.actions = new Dictionary<string, Dictionary<string,string>>();
        }

        /// <summary>
        /// Determines if a Resource can currently perform an action.
        /// </summary>
        /// <param name="name">Action to perform</param>
        /// <returns>True if the action exists in the actions dictionary, false otherwise</returns>
        public bool hasAction(string name)
        {
            return this.actions.ContainsKey(name);
        }

        /// <summary>
        /// Gets the current action url if it exists.
        /// </summary>
        /// <param name="name">Action to perform</param>
        /// <returns>The current action url or null if action not found</returns>
        public Dictionary<string,string> getAction(string name)
        {
            return hasAction(name) ? this.actions[name] : null;
        }

    }
}
