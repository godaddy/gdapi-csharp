using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gdapi
{
    class ItemResource : Resource
    {

        public Dictionary<string, string> actions { get; set; }

        /// <summary>
        /// Constructs a new Resource
        /// </summary>
        public ItemResource()
        {
            this.actions = new Dictionary<string, string>();
        }

        /// <summary>
        /// Creates a new Resource of a specific type. This constructor is usually used for creating a Resource to use for saving or deleting a Resource.
        /// </summary>
        /// <param name="type">Resource type</param>
        public ItemResource(string type)
        {
            this.actions = new Dictionary<string, string>();
            this.type = type;
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
        public string getAction(string name)
        {
            return hasAction(name) ? this.actions[name] : null;
        }

    }

}
