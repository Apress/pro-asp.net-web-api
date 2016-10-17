using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace HttpContentSample {
class Program {
    static void Main(string[] args) {
        var httpContent = new StringContent(@"
Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.
");
        Console.WriteLine(httpContent.ReadAsStringAsync().Result);
        Console.WriteLine("Initial size: {0} bytes", httpContent.Headers.ContentLength);

        var compressedContent = new CompressedContent(httpContent, "gzip");
        var result = compressedContent.ReadAsStringAsync().Result;
        Console.WriteLine("Compressed size: {0} bytes", compressedContent.Headers.ContentLength);
        Console.ReadLine();
    }
}
}
