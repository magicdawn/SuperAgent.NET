using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SuperAgent.Test.Request
{
    public class End
    {
        public SuperAgent.Request sampleRequest;
        public End()
        {
            sampleRequest = SuperAgent.Request.Get("http://www.baidu.com");
        }

        [Fact]
        public void End_test()
        {
            var res = sampleRequest
                        .End()
                        .Encoding("utf-8");

            var text = res.Text;

            Assert.Contains("百度",text);
        }
    }
}
