using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CompositionWithWaitAll {

    class Program {

        static void Main(string[] args) {

            //First async operation
            var httpClient = new HttpClient();
            Task<string> twitterTask = httpClient.GetStringAsync("http://twitter.com");

            //Second async operation
            var httpClient2 = new HttpClient();
            Task<string> googleTask = httpClient2.GetStringAsync("http://www.google.com");

            //blocks till all of the tasks are completed
            Task.WaitAll(twitterTask, googleTask);

            //all of the tasks has been completed. 
            //Reaching out to the Result property will not block.
            Console.WriteLine(twitterTask.Result);
            Console.WriteLine(googleTask.Result);
        }
    }
}