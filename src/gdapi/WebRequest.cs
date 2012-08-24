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
using System.Text;
using System.Net;
using System.IO;
using System.Web;

namespace gdapi
{
    /// <summary>
    /// Represents a WebRequest to the API
    /// </summary>
    public class WebRequest
    {
        private Client m_cClient = null;
        private string m_sType = "GET";
        private string m_sQuery = "";
        private string m_sUrl = "";
        private string m_sBody = "";

        private List<KeyValuePair<string, string>> m_lParams = new List<KeyValuePair<string, string>>();

        /// <summary>
        /// Creates a new WebRequest
        /// </summary>
        /// <param name="client">Client to use for all communications.</param>
        public WebRequest(Client client)
        {
            this.m_cClient = client;
            this.m_sUrl = client.getBaseUrl();
        }

        /// <summary>
        /// Sets the request body.
        /// </summary>
        /// <param name="body">String representing the request body.</param>
        public void setBody(string body)
        {
            this.m_sBody = body;
        }

        /// <summary>
        /// Sets the type of the request.
        /// </summary>
        /// <param name="type">Type to set the request to, most likely GET, PUT, or POST.</param>
        public void setType(string type)
        {
            this.m_sType = type;
        }

        /// <summary>
        /// Sets the query string of the request.
        /// </summary>
        /// <param name="query">Query string to append to the request.</param>
        public void setQuery(string query)
        {
            this.m_sQuery = query;
        }

        /// <summary>
        /// Add a parameter to the query string of the request.
        /// </summary>
        /// <param name="kvp">A key value pair to append to the query string of the request.</param>
        public void addParam(KeyValuePair<string, string> kvp)
        {
            this.m_lParams.Add(kvp);
        }

        /// <summary>
        /// Sets the Url of the request.
        /// </summary>
        /// <param name="url">
        /// Full url to set the url to.
        /// </param>
        public void setUrl(string url)
        {
            this.m_sUrl = url;
        }

        /// <summary>
        /// Returns the complete response body or the request. 
        /// </summary>
        /// <exception cref="NotFoundException">
        /// If the request returns a 404 error a NotFoundException is thrown. 
        /// </exception>
        public string getResponse()
        {

            string url = this.m_sUrl+this.m_sQuery;

            HttpWebRequest req = System.Net.WebRequest.Create(url) as HttpWebRequest;
            req.Credentials = this.m_cClient.getNetworkCredential();
            req.Method = this.m_sType;

            if (this.m_sType == "GET" && this.m_lParams.Count > 0)
            {
                url += "?";

                foreach (KeyValuePair<string, string> kvp in this.m_lParams)
                {
                   url += kvp.Key + "=" + HttpUtility.UrlEncode( kvp.Value );
                }
            }
            else if (this.m_sType == "POST" || this.m_sType == "PUT")
            {
                req.ContentType = "application/json";
                req.Accept = "application/json";

                
                ASCIIEncoding encoding=new ASCIIEncoding();
                byte[] bytes = encoding.GetBytes(this.m_sBody);

                req.ContentLength = bytes.Length;

                Stream dataStream = req.GetRequestStream();
                dataStream.Write(bytes, 0, bytes.Length);
                dataStream.Close();

            }
            
            string result = null;

            try
            {
                HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
                StreamReader reader = new StreamReader(resp.GetResponseStream());
                result = reader.ReadToEnd();
            }
            catch (WebException e)
            {
                HttpWebResponse webResponse = (HttpWebResponse)e.Response;
                if (webResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new NotFoundException();
                }
            }
            catch
            {
                throw;
            }
            return result;   

        }

    }
}
