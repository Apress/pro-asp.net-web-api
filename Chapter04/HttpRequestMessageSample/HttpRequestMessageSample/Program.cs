using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestMessageSample {
    internal class Program {
        private static void Main(string[] args) {
            var request =
                new HttpRequestMessage(HttpMethod.Get,
                                       new Uri("http://apress.com"));
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
            request.Headers.Add("X-Name", "Microsoft");
            Console.WriteLine(request.ToString());
            Console.ReadLine();
        }
    }
}