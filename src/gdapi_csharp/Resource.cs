/*
 * Copyright (c) 2012 Go Daddy Operating Company, LLC
 *
 * Permission is hereby granted, free of charge, to any person obtaining a
 * copy of this software and associated documentation files (the "Software"),
 * to deal in the Software without restriction, including without limitation
 * the rights to use, copy, modify, merge, publish, distribute, sublicense,
 * and/or sell copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
 * THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 * DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Collections.Generic;

namespace gdapi_csharp
{

    /// <summary>
    /// Represents a Resource from the API. This class is usually automatically populated by ResourceConverter from the returned JSON string.
    /// </summary>
    /// <seealso cref="ResourceConverter"/>
    public class Resource
    {

        private Dictionary<string, string> m_dProperties = new Dictionary<string, string>();
        public Dictionary<string, string> actions { get; set; }
        public Dictionary<string, string> links { get; set; }
        public String type { get; set; }

        /// <summary>
        /// Constructs a new Resource
        /// </summary>
        public Resource()
        {
            this.actions = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets the properties of the Resource.
        /// </summary>
        /// <returns>The properties from the Resource</returns>
        public Dictionary<string, string> getProperties()
        {
            return this.m_dProperties;
        }

        /// <summary>
        /// Creates a new Resource of a specific type. This constructor is usually used for creating a Resource to use for saving or deleting a Resource.
        /// </summary>
        /// <param name="type">Resource type</param>
        public Resource(string type)
        {
            this.actions = new Dictionary<string, string>();
            this.type = type;
        }

        /// <summary>
        /// Sets the type of the Resource
        /// </summary>
        /// <param name="type">Resource type</param>
        public void setType(string type)
        {
            this.type = type;
        }

        /// <summary>
        /// Gets the type of the Resource
        /// </summary>
        /// <returns>Resource type</returns>
        public String getType()
        {
            return this.type;
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

        /// <summary>
        /// Determins if a Resource currently has a link.
        /// </summary>
        /// <param name="name">Link to check</param>
        /// <returns>True if the link exists in the links dictionary, false otherwise</returns>
        public bool hasLink(string name)
        {
            return this.links.ContainsKey(name);
        }

        /// <summary>
        /// Gets the current link if it exists.
        /// </summary>
        /// <param name="name">Link to fetch</param>
        /// <returns>The current link url or null if link not found</returns>
        public string getLink(string name)
        {
            return hasLink(name) ? this.links[name] : null;
        }

        /// <summary>
        /// Sets the property of a resource
        /// </summary>
        /// <param name="key">Property name to set</param>
        /// <param name="value">Property value to set</param>
        public void setProperty(string key, string value)
        {
            if (this.m_dProperties.ContainsKey(key))
            {
                this.m_dProperties[key] = value;
            }
            else
            {
                this.m_dProperties.Add(key, value);
            }
        }

        /// <summary>
        /// Gets the current property value
        /// </summary>
        /// <param name="propertyName">Name of property to get</param>
        /// <returns>Value of property</returns>
        public string getProperty(string propertyName)
        {
            return m_dProperties[propertyName];
        }

    }

}
