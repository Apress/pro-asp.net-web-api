using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace EAPSample {

    class Program {

        static void Main(string[] args) {

            WebClient client = new WebClient();

            client.DownloadStringCompleted += (sender, eventArgs) => {

                Console.WriteLine(eventArgs.Result);
            };

            client.DownloadStringAsync(new Uri("http://example.com"));

            Console.ReadLine();
        }

    }
}