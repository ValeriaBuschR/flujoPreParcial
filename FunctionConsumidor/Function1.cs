
namespace FunctionConsumidor
{
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using FunctionConsumidor.Models;
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([ServiceBusTrigger("cola", Connection = "MyConn")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
            var data = JsonConvert.DeserializeObject<Odometro>(myQueueItem);
        }
    }
}
