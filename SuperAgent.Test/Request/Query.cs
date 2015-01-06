using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SuperAgent.Test.Request
{
    public class Query
    {
        public SuperAgent.Request sampleRequest;

        public Query()
        {
            sampleRequest = SuperAgent.Request.Get("http://www.baidu.com");
        }

        [Theory]
        [InlineData("name=zhang&age=18")]
        public void Query_string(string query)
        {
            sampleRequest.Query(query);

            Assert.Contains(sampleRequest.queryString,(pair) => {
                return pair.Key == "name" && pair.Value == "zhang";
            });

            Assert.Contains(sampleRequest.queryString,(pair) => {
                return pair.Key == "age" && pair.Value == "18";
            });
        }

        [Theory]
        [InlineData("name","zhang")]
        public void Query_string_string(string key,string value)
        {
            sampleRequest.Query(key,value);
            Assert.Contains(sampleRequest.queryString,pair => {
                return pair.Key == "name" && pair.Value == "zhang";
            });
        }

        [Fact]
        public void Query_object()
        {
            sampleRequest.Query(new { 
                name = "zhang",
                age = 18 
            });

            Assert.Contains(sampleRequest.queryString,pair => {
                return pair.Key == "name" && pair.Value == "zhang";
            });

            Assert.Contains(sampleRequest.queryString,pair=>{
                return pair.Key == "age" && pair.Value == "18";
            });
        }
    }
}
