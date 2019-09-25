using System;
using Microsoft.Azure.ServiceBus.Management;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionApp
{
    public static class AzureServiceBusITriggerFunc
    {
        [FunctionName("AzureServiceBusITriggerFunc")]
        public static void Run([ServiceBusTrigger("functriggerq",Connection = "Endpoint=sb://azureservicebusforpoc.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=A3QjeITGFgm4BaKEHE+dpvFA6rzI2SoQmUDEaxqt9IE=")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
