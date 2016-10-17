using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace HttpClientFormsAuthSample.Client {
    internal class Program {
        private static void Main(string[] args) {

            Console.WriteLine("Username:");
            var username = Console.ReadLine();
            Console.WriteLine("Password:");
            var password = Console.ReadLine();

            var client = new HttpClient();

            // setup initial authentication request
            var authRequest = new HttpRequestMessage() {
                Method = HttpMethod.Post,
                RequestUri = new Uri("http://localhost:3915/Account/Login"),
                Content = new FormUrlEncodedContent(
                    new List<KeyValuePair<string, string>> {                            
                    new KeyValuePair<string, string>("Username", username), 
                    new KeyValuePair<string, string>("Password", password)
            })
            };

            // try to authenticate
            var authResponse = client.SendAsync(authRequest).Result;
            IEnumerable<string> values;
            authResponse.Headers.TryGetValues("Set-Cookie", out values);


            if (null == values || string.IsNullOrEmpty(values.First())) {
                Console.WriteLine("Username and password must equal.");
                Console.ReadLine();
                return;
            }

            var cookie = values.First();

            // setup request to retrieve data from the server
            var request = new HttpRequestMessage() {
                RequestUri = new Uri("http://localhost:3915/customer/get/1")
            };

            // assign cookie
            request.Headers.Add("Cookie", cookie);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.SendAsync(request).Result;
            Console.WriteLine("Customer: {0}", response.Content.ReadAsAsync<Customer>().Result.Name);
            Console.ReadLine();
        }
    }
}
