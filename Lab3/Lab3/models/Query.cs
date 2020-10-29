using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.models
{
   public class Query
    {
        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }

        public Query()
        {
            this.Field1 = "";
            this.Field2 = "";
            this.Field3 = "";
        }
    }
}
