using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Frequency
    {
        

        public Frequency(string v1, int v2)
        {
            this.Name = v1;
            this.Sum = v2;
        }

        public string Name { get; set; }
        public int Sum { get; set; }
    }
}
