using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Pair
    {
       

        public Pair(string v1, string v2)
        {
            this.Name1 = v1;
            this.Name2 = v2;
        }

        public string Name1 { get; set; }
        public string Name2 { get; set; }
    }
}
