using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace HttpClientGetSample {
    class Program {
        static void Main(string[] args) {
            var httpClient = new HttpClient();
            var uri = "https://raw.github.com/AlexZeitler/HttpClient/master/README.md";
            var ipsum = httpClient.GetStringAsync(uri).Result;
            Console.WriteLine(ipsum);
            Console.ReadLine();
        }
    }
}
