using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            SuperAgent.Request.Get("http://www.baidu.com")
                .Query(new { 
                    name = "zhang",
                    age = 18
                });
        }
    }
}
