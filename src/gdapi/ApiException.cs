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

namespace gdapi
{
    /// <summary>
    /// Represents an exception returned from the API
    /// </summary>
    public class ApiException : ApplicationException
    {

        private string m_sMessage = null;
        private string m_sCode = null;
        private string m_sDetail = null;

        public ApiException(string message, string code, string detail) : base(message)
        {
            this.m_sMessage = message;
            this.m_sCode = code;
            this.m_sDetail = detail;
        }

        public string getMessage()
        {
            return this.m_sMessage;
        }

        public string getCode()
        {
            return this.m_sCode;
        }

        public string getDetail()
        {
            return this.m_sDetail;
        }

    }

}
