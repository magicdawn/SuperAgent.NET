using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SuperAgent
{
    public class Util
    {
        // http://msdn.microsoft.com/ZH-CN/library/s71167ye(v=VS.100,d=hv.2).aspx
        public static void SetHeader(HttpWebRequest request,Dictionary<string,string> headers)
        {
            foreach(var item in headers)
            {
                var key = item.Key;
                var val = item.Value;

                switch(key.ToLowerInvariant())
                {
                    case "accept":
                        request.Accept = val;
                        break;
                    case "connection":
                        request.Connection = val;
                        break;
                    case "content-length":
                        request.ContentLength = 10;
                        break;
                    case "content-type":
                        request.ContentType = val;
                        break;
                    case "expect":
                        request.Expect = val;
                        break;
                    case "date":
                        // TODO: 可配置
                        request.Date = DateTime.Now;
                        break;
                    case "host":
                        request.Host = val;
                        break;
                    case "if-modified-since":
                        // TODO: 可配置
                        request.IfModifiedSince = DateTime.Now;
                        break;
                    case "range":
                        // TODO: 添加range
                        break;
                    case "referer":
                        request.Referer = val;
                        break;
                    case "transfer-encoding":
                        request.TransferEncoding = val;
                        break;
                    case "user-agent":
                        request.UserAgent = val;
                        break;
                    case "":
                        break;
                    default:
                        request.Headers[key] = val;
                        break;
                }
            }
        }

        public static Dictionary<string,string> MimeMap = new Dictionary<string,string>() {
            { "form", "application/x-www-form-urlencoded" },
            { "part","multipart/form-data" },
            { "json","application/json" },
            { "text","plain/text" }
        };
    }
}