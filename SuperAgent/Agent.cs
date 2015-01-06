using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace SuperAgent
{
    public class Agent
    {
        #region static member

        // default agent
        public static Agent defaultAgent = new Agent() {
            // chrome that I'm using
            AgentName = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/38.0.2125.111 Safari/537.36"
        };

        #endregion

        #region class member

        public Agent(string agentName = "",CookieContainer cookies = null)
        {
            if(string.IsNullOrEmpty(agentName))
            {
                this.AgentName = "SuperAgent.NET";
            }
            else
            {
                this.AgentName = agentName;
            }

            if(cookies == null)
            {
                this.Cookies = new CookieContainer();
            }
            else
            {
                this.Cookies = cookies;
            }
        }

        // user-agent
        public string AgentName;
        // cookie-jar
        public CookieContainer Cookies;

        public Request Get(string url)
        {
            return new Request(this,"GET",url);
        }

        public Request Post(string url)
        {
            return new Request(this,"POST",url);
        }

        #endregion
    }
}
