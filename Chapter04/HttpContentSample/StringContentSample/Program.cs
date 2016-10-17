using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace StringContentSample {
class Program {
    static void Main(string[] args) {
        var stringContent = new StringContent("Hello World");
        Console.WriteLine(stringContent.ReadAsStringAsync().Result);
        Console.ReadLine();
    }
}
}
