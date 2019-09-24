using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FuncAppConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            InvokeMethod().Wait();
            
        }

        public static async Task<HttpResponseMessage> InvokeMethod()
        {
            //Call API
            HttpClient httpClient = new HttpClient();
           // HttpRequestMessage objreq = new HttpRequestMessage(HttpMethod.Get, "http://dummy.restapiexample.com/api/v1/employees");

            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("http://dummy.restapiexample.com/api/v1/employees");

            string res = await httpResponseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject(res);

            //HttpResponseMessage response = httpClient.GetAsync("api/Department/1");



            return httpResponseMessage;
        }
    }
}
