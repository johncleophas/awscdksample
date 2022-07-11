using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCdkLambda
{
    public class RequestBody
    {
        public string Field1 { get; set; }
        public string Field2 { get; set; }

        public RequestBody()
        {
            this.Field1 = "";
            this.Field2 = "";
        }
    }
}
