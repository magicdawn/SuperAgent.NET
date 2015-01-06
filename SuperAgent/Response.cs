using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace SuperAgent
{
    public class Response
    {
        public HttpWebRequest Request; // request
        public HttpWebResponse OriginalResponse; // 原始response

        public Response(HttpWebRequest request,HttpWebResponse originalResponse)
        {
            this.Request = request;
            this.OriginalResponse = originalResponse;
        }

        string text;
        public string Text
        {
            get
            {
                if(string.IsNullOrEmpty(text))
                {
                    var s = OriginalResponse.GetResponseStream();

                    if(this.encoding != null)
                    {
                        text = new StreamReader(s,this.encoding).ReadToEnd();
                    }
                    else
                    {
                        text = new StreamReader(s,true).ReadToEnd();
                    }
                }
                return text;
            }
        }

        private Encoding encoding;
        public Response Encoding(Encoding encoding)
        {
            this.encoding = encoding;
            return this;
        }
        public Response Encoding(string encoding)
        {
            this.encoding = System.Text.Encoding.GetEncoding(encoding);
            return this;
        }
    }
}