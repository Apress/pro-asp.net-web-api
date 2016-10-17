using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CompositionWithWhenAll {

    class Program {

        static void Main(string[] args) {

            //First async operation
            var httpClient = new HttpClient();
            Task<string> twitterTask =
                httpClient.GetStringAsync("http://twitter.com");

            //Second async operation
            var httpClient2 = new HttpClient();
            Task<string> googleTask =
                httpClient2.GetStringAsync("http://google.com");

            Task<string[]> task = Task.WhenAll(twitterTask, googleTask);

            task.ContinueWith(stringArray => {

                //all of the tasks have been completed. 
                //Reaching out to the Result property will not block.

                if (task.Status == TaskStatus.RanToCompletion) {

                    for (int i = 0; i < stringArray.Result.Length; i++) {

                        Console.WriteLine(stringArray.Result[i].Substring(0, 100));
                    }
                }
                else if (task.Status == TaskStatus.Canceled) {

                    Console.WriteLine("The task has been canceled. ID: {0}", task.Id);
                }
                else {
                    Console.WriteLine("An error has been occurred. Details:");
                    foreach (var ex in task.Exception.InnerExceptions) {
                        Console.WriteLine(ex.Message);
                    }
                }
            });

            Console.ReadLine();
        }
    }
}
