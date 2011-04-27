using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReadFromFile
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = new XmlParser("Data");
            var r = t.GetData("abb");
            Console.WriteLine("Count = " + r.Count());
        }
    }
}
