using System.Net;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Extensions.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Function
{
    public class HttpTrigger2
    {
        private readonly ILogger _logger;

        public HttpTrigger2(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HttpTrigger2>();
        }

        [Function("HttpTrigger2")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req, FunctionContext context)
        {
            _logger.LogInformation($"C# HTTP trigger function processed a request for {context.InvocationId}.");

            var serviceBusClient = context.InstanceServices.GetRequiredService<ServiceBusClient>();
            var serviceBusSender = serviceBusClient.CreateSender("outputQueue");
            await serviceBusSender.SendMessageAsync(new ServiceBusMessage($"body of the Service Bus response for {context.InvocationId}."));

            HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync($"body of the HTTP response for {context.InvocationId}.");
            
            return response;
        }
    }
}
