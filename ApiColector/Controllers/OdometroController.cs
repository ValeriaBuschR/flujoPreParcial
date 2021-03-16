namespace ApiColector.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Azure.Messaging.ServiceBus;
    using ApiColector.Models;
    using Newtonsoft.Json;


    [Route("api/[controller]")]
    [ApiController]
    public class OdometroController : ControllerBase
    {
        [HttpPost]
        public async Task<bool> EnviarAsync([FromBody] Odometro odometro)
        {
            string connectionString = "Endpoint=sb://buspreparcial.servicebus.windows.net/;SharedAccessKeyName=enviar;SharedAccessKey=ZPD1NqULBBiusxZ4LaDOIiJe7SPn1dlRUSJJ+8fImlo=;EntityPath=cola";
            string queueName = "cola";

            string Mensaje = JsonConvert.SerializeObject(odometro);
            // create a Service Bus client 
            await using (ServiceBusClient client = new ServiceBusClient(connectionString))
            {
                // create a sender for the queue 
                ServiceBusSender sender = client.CreateSender(queueName);

                // create a message that we can send
                ServiceBusMessage message = new ServiceBusMessage(Mensaje);

                // send the message
                await sender.SendMessageAsync(message);
                Console.WriteLine($"Sent a single message to the queue: {queueName}");
            }
            return true;
        }
    }
}

