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
using System.Reflection;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace gdapi
{

    /// <summary>
    /// Represents a JsonConverter which converts the returned JSON from the API to the correct Object.
    /// </summary>
    public class ResourceConverter : JsonConverter
    {

        private static Type RESOURCE_FILTER = typeof(Dictionary<string, List<KeyValuePair<string, string>>>);
        private static Type RESOURCE_DICTIONARY = typeof(Dictionary<string, string>);
        private static Type RESOURCE_LIST = typeof(List<Resource>);
        private static Type RESOURCE_STRING = typeof(String);

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {

            JObject jObject = JObject.Load(reader);

            Object result = null;

            if (objectType == typeof(Collection))
            {
                result = new Collection();
            }
            else if (objectType == typeof(Resource))
            {
                result = new Resource();

                foreach (JProperty property in jObject.Properties())
                {
                    if (objectType.GetProperty(property.Name) == null)
                    {
                        ((Resource)result).setProperty(property.Name, property.Value.ToString());
                    }
                }
            
            }

            foreach (PropertyInfo property in objectType.GetProperties())
            {
                if (jObject.Property(property.Name) != null)
                {
                    Type propertyType = property.PropertyType;
                    if (propertyType == ResourceConverter.RESOURCE_FILTER)
                    {
                        Dictionary<string, List<KeyValuePair<string, string>>> dFilters = new Dictionary<string, List<KeyValuePair<string, string>>>();
                        foreach (JProperty p in jObject.Property(property.Name).Value)
                        {
                            List<KeyValuePair<string, string>> lFilterValues = new List<KeyValuePair<string, string>>();

                            if (p.Value.HasValues)
                            {
                                foreach (JToken jToken in p.Values())
                                {
                                    lFilterValues.Add(new KeyValuePair<string, string>("value", jToken["value"].ToString()));
                                }
                            }

                            dFilters.Add(p.Name, lFilterValues);
                        }
                        property.SetValue(result, dFilters, null);
                    }
                    else if (propertyType == ResourceConverter.RESOURCE_STRING)
                    {
                        property.SetValue(result, jObject.Property(property.Name).Value.ToString(),null);
                    }
                    else if (propertyType == ResourceConverter.RESOURCE_DICTIONARY)
                    {
                        property.SetValue(result, jObject.Property(property.Name).Value.ToObject<Dictionary<string, string>>(), null);
                    }
                    else if (propertyType == ResourceConverter.RESOURCE_LIST)
                    {

                        JArray aResources = (JArray)jObject.Property(property.Name).Value;
                        List<Resource> lResources = new List<Resource>();
                        foreach (JToken jToken in aResources)
                        {
                            lResources.Add((Resource)JsonConvert.DeserializeObject(jToken.ToString(), typeof(Resource), new ResourceConverter()));
                        }

                        property.SetValue(result, lResources, null);
                    }
                }
            }

            return result;

        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

    }

}

