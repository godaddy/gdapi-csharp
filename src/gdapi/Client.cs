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

using System.Runtime.Remoting;
using System.Net;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace gdapi
{
    /// <summary>
    /// Represents the Client which handles all communication to the API.
    /// </summary>
    public class Client
    {

        private string m_sBaseUrl;
        private string m_sAccessKey;
        private string m_sSecretKey;

        /// <summary>
        /// Creates a new Client.
        /// </summary>
        /// <param name="base_url">Base Url of the API</param>
        /// <param name="access_key">Access Key to the API</param>
        /// <param name="secret_key">Secret Key to the API</param>
        public Client(string base_url, string access_key, string secret_key)
        {
            this.m_sBaseUrl = base_url;
            this.m_sAccessKey = access_key;
            this.m_sSecretKey = secret_key;
        }

        /// <summary>
        /// Returns a filtered Collection from the API
        /// </summary>
        /// <param name="resourceType">Type of the Collection to return.</param>
        /// <param name="filters">Additional filters for the Collection.</param>
        /// <returns>Collection from API</returns>
        public Collection getCollection(string resourceType, CollectionFilter collectionFilter)
        {
            gdapi.WebRequest rRequestor = new gdapi.WebRequest(this);
            rRequestor.setType("GET");
            rRequestor.setQuery(resourceType);

            if (collectionFilter != null)
            {
                foreach(string key in collectionFilter.getFilterItems().Keys)
                {
                    foreach (Dictionary<string, string> filter in collectionFilter.getFilterItems()[key])
                    {
                        rRequestor.addParam(new KeyValuePair<string, string>(key + "_" + filter["modifier"], filter["value"]));
                    }
                }                    
            }

            return getCollectionByRequest(rRequestor);
        }

        /// <summary>
        /// Returns an unfiltered Collection from the API 
        /// </summary>
        /// <param name="resourceType">Type of the Collection to return.</param>
        /// <returns></returns>
        public Collection getCollection(string resourceType)
        {
            return getCollection(resourceType, null);

        }

        /// <summary>
        /// Fetches a link from the API as a Collection.
        /// </summary>
        /// <param name="resource">Resource object which contains links.</param>
        /// <param name="linkName">Link name to return.</param>
        /// <exception cref="NotFoundException"/>
        /// <returns>Collection from Resource</returns>
        public Collection fetchLinkAsCollection(Resource resource, string linkName)
        {
            if (resource.hasLink(linkName))
            {
                gdapi.WebRequest rRequestor = new gdapi.WebRequest(this);
                rRequestor.setType("GET");
                rRequestor.setUrl(resource.getLink(linkName));
                return getCollectionByRequest(rRequestor);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Fetches a link from the API as a Resource.
        /// </summary>
        /// <param name="resource">Resource object which contains links.</param>
        /// <param name="linkName">Link name to return.</param>
        /// <exception cref="NotFoundException"/>
        /// <returns>Resource from Resource</returns>
        public Resource fetchLinkAsResource(Resource resource, string linkName)
        {
            if (resource.hasLink(linkName))
            {
                gdapi.WebRequest rRequestor = new gdapi.WebRequest(this);
                rRequestor.setType("GET");
                rRequestor.setUrl(resource.getLink(linkName));
                return getResourceByRequest(rRequestor);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Performs an action from a Resource and returns the resulting Resource.
        /// </summary>
        /// <param name="actionUrl">Action URL to perform.</param>
        /// <returns>Resource result of the action.</returns>
        public Resource doAction(string actionUrl)
        {
            return this.doAction(actionUrl, null);
        }

        /// <summary>
        /// Performs an action from an action url and an input resource and returns the resulting Resource
        /// </summary>
        /// <param name="actionUrl">Action URL to perform.</param>
        /// <param name="inputResource">Input Resource to use send with the action URL.</param>
        /// <returns>Resource result of the action.</returns>
        public Resource doAction(string actionUrl, Resource inputResource)
        {
            gdapi.WebRequest rRequestor = new gdapi.WebRequest(this);
            rRequestor.setType("POST");
            rRequestor.setUrl(actionUrl);
            if (inputResource != null)
            {
                rRequestor.setBody(JsonConvert.SerializeObject(inputResource.getProperties()));
            }
            return getResourceByRequest(rRequestor);
        }

        /// <summary>
        /// Creates a new Resource.
        /// </summary>
        /// <param name="collectionType">Collection type to add the resource to</param>
        /// <param name="resource">Resource to add to the Collection</param>
        /// <returns>Newly created Resource</returns>
        public Resource create(string collectionType, Resource resource)
        {
            gdapi.WebRequest rRequestor = new gdapi.WebRequest(this);
            rRequestor.setType("POST");
            rRequestor.setBody(JsonConvert.SerializeObject(resource.getProperties()));
            rRequestor.setQuery(collectionType);
            return getResourceByRequest(rRequestor);
        }

        /// <summary>
        /// Deletes a Resource
        /// </summary>
        /// <param name="collectionType">Collection type to delete the resource from</param>
        /// <param name="resource">Resource to delete</param>
        /// <returns>Deleted Resource</returns>
        public Resource delete(string collectionType, Resource resource)
        {
            gdapi.WebRequest rRequestor = new gdapi.WebRequest(this);
            rRequestor.setType("DELETE");
            rRequestor.setQuery(collectionType + "/" + resource.getProperty("id"));
            return getResourceByRequest(rRequestor);
        }

        /// <summary>
        /// Saves a Resource
        /// </summary>
        /// <param name="collectionType">Collection type to save the resource to</param>
        /// <param name="resource">Resource to save</param>
        /// <returns>Saved Resource</returns>
        public Resource save(string collectionType, Resource resource)
        {
            if (resource.hasProperty("id"))
            {
                gdapi.WebRequest rRequestor = new gdapi.WebRequest(this);
                rRequestor.setType("PUT");
                rRequestor.setBody(JsonConvert.SerializeObject(resource.getProperties()));
                rRequestor.setQuery(collectionType + "/" + resource.getProperty("id"));
                return getResourceByRequest(rRequestor);
            }
            else
            {
                return create(collectionType, resource);
            }
        }

        /// <summary>
        /// Returns a Collection from a WebRequest.
        /// </summary>
        /// <param name="webRequest">WebRequest to get the response from</param>
        /// <returns>The Collection representation of the JSON response or null if there was an error parsing the returned JSON.</returns>
        private Collection getCollectionByRequest(gdapi.WebRequest webRequest)
        {
             string sResponse = webRequest.getResponse();
             return (Collection)JsonConvert.DeserializeObject(sResponse, typeof(Collection), new ResourceConverter());
        }

        /// <summary>
        /// Returns a Resource from a WebRequest
        /// </summary>
        /// <param name="webRequest">WebRequest to get the response from</param>
        /// <returns>The Resource representation of the JSON response or null if there was an error parsing the returned JSON.</returns>
        private Resource getResourceByRequest(gdapi.WebRequest webRequest)
        {
            Resource result = null;
            string sResponse = webRequest.getResponse();
            result = (Resource)JsonConvert.DeserializeObject(sResponse, typeof(Resource), new ResourceConverter());
            return result;
        }

        /// <summary>
        /// Returns a Resource by an id.
        /// </summary>
        /// <param name="collectionType">Collection to pull the Resource from</param>
        /// <param name="resourceId">Id of the Resource to pull</param>
        /// <returns>The Resource represented by resourceId</returns>
        public Resource getResourceById(string collectionType, string resourceId)
        {
            gdapi.WebRequest rRequestor = new gdapi.WebRequest(this);
            rRequestor.setType("GET");
            rRequestor.setQuery(collectionType+"/"+resourceId);
            return getResourceByRequest(rRequestor);
        }

        /// <summary>
        /// Returns the current NetworkCredentials used by the Client
        /// </summary>
        /// <returns>NetworkCredentials of the Client.</returns>
        public NetworkCredential getNetworkCredential()
        {
            return new NetworkCredential(this.m_sAccessKey, this.m_sSecretKey);
        }

        /// <summary>
        /// Gets the base url of the Client
        /// </summary>
        /// <returns>Base url of the Client</returns>
        public string getBaseUrl()
        {
            return this.m_sBaseUrl;
        }
    }
}
