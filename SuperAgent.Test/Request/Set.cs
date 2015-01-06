using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SuperAgent.Test.Request
{
    public class Set
    {
        SuperAgent.Request sampleRequest;
        public Set()
        {
            sampleRequest = SuperAgent.Request.Get("http://www.baidu.com");
        }

        [Theory]
        [InlineData("User-Agent","test-agent/0.0.1")]
        public void Set_string_string(string name,string value)
        {
            sampleRequest.Set(name,value);
            Assert.Contains(sampleRequest.Headers,pair => {
                return pair.Key == "user-agent" && pair.Value == value;
            });
        }

        [Theory]
        [InlineData("form")]
        public void Type(string val)
        {
            var res = sampleRequest
                .Type(val)
                .End();

            Assert.Equal(sampleRequest.Req.ContentType,SuperAgent.Util.MimeMap["form"]);
        }
    }
}