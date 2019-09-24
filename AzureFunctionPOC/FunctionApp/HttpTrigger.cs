using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;

namespace FunctionApp
{
    public static class HttpTrigger
    {
        [FunctionName("HttpTrigger")]
        public static async Task<string> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogCritical("Test");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

           HttpResponseMessage httpResponseMessage=  await ThirdPartyInvoke();
            var response = await httpResponseMessage.Content.ReadAsStringAsync();

            return response;
           // return name != null
            //    ? (ActionResult)new OkObjectResult($"Hello Welcome, {name} ")
              //  : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }

        public static async Task<HttpResponseMessage> ThirdPartyInvoke()
        {
            HttpClient objClient = new HttpClient();

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://dummy.restapiexample.com/api/v1/employees");

            HttpResponseMessage httpResponseMessage = await objClient.SendAsync(httpRequestMessage);

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                var response = await httpResponseMessage.Content.ReadAsStringAsync();
                Console.WriteLine(response);
            }
            else
            {
                Console.WriteLine("Got error response from API");
            }
            return httpResponseMessage;


        }
    }
}
