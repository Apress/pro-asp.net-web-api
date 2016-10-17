using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace HttpClientConneg.Client {
    class Program {
        static void Main(string[] args) {
            var client = new HttpClient();
            var response = client.GetAsync("http://localhost:3739/api/customer/1").Result;
            var customer = response.Content.ReadAsAsync<Customer>().Result;
            Console.WriteLine("Id: {0}, Name: {1}", customer.Id, customer.Name);
            Console.ReadLine();
        }
    }
}
