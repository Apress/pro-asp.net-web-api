using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CompositionContinueWhenAll {

    class Program {

        static void Main(string[] args) {

            //First async operation
            var httpClient = new HttpClient();
            Task<string> twitterTask = 
                httpClient.GetStringAsync("http://twitter.com");

            //Second async operation
            var httpClient2 = new HttpClient();
            Task<string> googleTask = 
                httpClient2.GetStringAsync("http://www.google.com");

            Task.Factory.ContinueWhenAll(new[] { twitterTask, googleTask }, (tasks) => {

                //all of the tasks have been completed. 
                //Reaching out to the Result property will not block.

                foreach (var task in tasks) {

                    if (task.Status == TaskStatus.RanToCompletion) {

                        Console.WriteLine(task.Result.Substring(0, 100));
                    }
                    else if (task.Status == TaskStatus.Canceled) {

                        Console.WriteLine("The task has been canceled. ID: {0}", task.Id);
                    }
                    else {
                        Console.WriteLine("An error has been occurred. Details:");
                        Console.WriteLine(task.Exception.InnerException.Message);
                    }
                }
            });

            Console.ReadLine();
        }
    }
}
