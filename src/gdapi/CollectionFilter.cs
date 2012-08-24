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
    /// <summary>
    /// Represents a filter to be used with a request for a Collection.
    /// </summary>
    public class CollectionFilter
    {

        private Dictionary<string, List<Dictionary<string, string>>> m_dFilterItems = new Dictionary<string, List<Dictionary<string, string>>>();

        /// <summary>
        /// Creates a new CollectionFilter
        /// </summary>
        public CollectionFilter()
        {

        }

        /// <summary>
        /// Creates a new CollectionFilter with a filter with property, modifier, and value.
        /// </summary>
        /// <param name="property">The property of the Resource to filter by</param>
        /// <param name="modifier">The modifier type</param>
        /// <param name="value">The value to filter by</param>
        public CollectionFilter(string property, string modifier, string value)
        {
            addFilterItem(property, modifier, value);
        }

        /// <summary>
        /// Adds a filter item to the filter
        /// </summary>
        /// <param name="property">The property of the Resource to filter by</param>
        /// <param name="modifier">The modifier type</param>
        /// <param name="value">The value to filter by</param>
        public void addFilterItem(string property, string modifier, string value)
        {

            List<Dictionary<string, string>> filterList = new List<Dictionary<string, string>>();
            Dictionary<string, string> filterItem = new Dictionary<string, string>();
            filterItem.Add("modifier", modifier);
            filterItem.Add("value", value);
            filterList.Add(filterItem);

            m_dFilterItems[property] = filterList;
        }

        /// <summary>
        /// Gets the filter items
        /// </summary>
        /// <returns>Dictionary of the filter items</returns>
        public Dictionary<string, List<Dictionary<string, string>>> getFilterItems()
        {
            return this.m_dFilterItems;
        }

    }

}
