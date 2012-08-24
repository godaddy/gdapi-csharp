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

using System.Collections;
using System.Collections.Generic;

namespace gdapi
{
    /// <summary>
    /// Represents a Collection from the API. This class is usually automatically populated by ResourceConverter from the returned JSON string.
    /// </summary>
    /// <seealso cref="ResourceConverter"/>
    public class Collection
    {

        public Dictionary<string, string> links { get; set; }
        public Dictionary<string, List<KeyValuePair<string,string>>> filters { get; set; }
        public Dictionary<string, string> resourceTypes { get; set; }
        public List<Resource> data { get; set; }

        /// <summary>
        /// Enumerates over the data of the Collection
        /// </summary>
        /// <returns>The data list of Resources from the Collection</returns>
        public IEnumerator GetEnumerator()
        {
            return data.GetEnumerator();
        }

        /// <summary>
        /// Gets the list of resources associated with the collection.
        /// </summary>
        /// <returns>List of resources</returns>
        public List<Resource> getResources()
        {
            return this.data;
        }

    }

}
