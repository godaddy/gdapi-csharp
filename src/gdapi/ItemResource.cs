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

using System.Collections.Generic;

namespace gdapi
{
    public class ItemResource : Resource
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
