using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.IO;

namespace SuperAgent
{
    public class Request
    {
        #region 发送请求 静态方法
        public static Request Get(string url)
        {
            return new Request(Agent.defaultAgent,"GET",url);
        }
        public static Request Post(string url)
        {
            return new Request(Agent.defaultAgent,"POST",url);
        }
        #endregion

        #region 构造函数
        public Agent Agent;
        public string Method;
        public string Url;
        public Request(Agent agent,string method,string url)
        {
            this.Agent = agent;
            this.Method = method;
            this.Url = url;
        }
        #endregion

        #region querystring
#if !RELEASE
        // 只有release的时候,把这些设为private
        public
#endif
 List<KeyValuePair<string,string>> queryString = new List<KeyValuePair<string,string>>();

        // 1. Query("name=zhang&age=18")
        public Request Query(string query)
        {
            var pairs = from pair in query.Split('&')
                    let arr = pair.Split('=')
                    select new KeyValuePair<string,string>(arr[0],arr[1]);
            this.queryString.AddRange(pairs);
            return this;
        }
        // 2. Query("name","zhangsan")
        public Request Query(string key,Object value)
        {
            this.queryString.Add(new KeyValuePair<string,string>(key,value.ToString()));
            return this;
        }
        // 3.Query(new {
        //      name = "zhang",
        //      age  = 18 
        //   })
        public Request Query(object o)
        {
            // get all key&val
            var props = o.GetType().GetProperties();
            foreach(var prop in props)
            {
                var name = prop.Name;
                var value = prop.GetValue(o,null).ToString();
                this.queryString.Add(new KeyValuePair<string,string>(name,value));
            }
            return this;
        }
        #endregion

        #region POST Send
        public List<KeyValuePair<string,string>> FormDatas = new List<KeyValuePair<string,string>>();
        public Request Send(string name,string value)
        {
            this.FormDatas.Add(new KeyValuePair<string,string>(name,value));
            return this;
        }

        public Request Send(object obj)
        {
            var props = obj.GetType().GetProperties();
            foreach (var prop in props)
	        {
		        this.Send(prop.Name,prop.GetValue(obj,null).ToString());
	        }
            return this;
        }
        #endregion

        #region headers
        public Dictionary<string,string> Headers = new Dictionary<string,string>();
        public Request Set(string key,string value)
        {
            key = key.ToLowerInvariant();
            this.Headers[key] = value;
            return this;
        }

        public Request Type(string type)
        {
            // form -> urlencoded
            while(Util.MimeMap.ContainsKey(type))
            {
                type = Util.MimeMap[type];
            }

            return this.Set("content-type",type);
        }

        public Request Accept(string accept)
        {
            return this.Set("accept",accept);
        }
        #endregion

        public HttpWebRequest Req;
        public Response End()
        {
            // url
            var url = this.Url;
            if(!url.StartsWith("http://") && !url.StartsWith("https://"))
            {
                url = "http://" + url;
            }

            // queryString
            if(this.queryString.Count > 0)
            {
                string query = string.Join("&",from pair in this.queryString
                                               select pair.Key + "=" + pair.Value);
                url += "?" + query;
            }

            this.Req = (HttpWebRequest)HttpWebRequest.Create(url);

            // headers
            Util.SetHeader(this.Req,this.Headers);

            // cookies
            Req.CookieContainer = this.Agent.Cookies;

            // FormData
            if(this.FormDatas.Count > 0)
            {
                string data = string.Join("&",from pair in this.FormDatas
                                               select pair.Key + "=" + pair.Value);

                data = System.Net.WebUtility.HtmlEncode(data);

                new StreamWriter(this.Req.GetRequestStream(),Encoding.UTF8).Write(data);
            }


            // response
            var originalResponse = (HttpWebResponse)this.Req.GetResponse();
            var res = new Response(this.Req,originalResponse);

            return res;
        }
    }
}
