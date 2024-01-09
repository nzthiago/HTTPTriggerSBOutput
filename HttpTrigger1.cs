using System.Net;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Extensions.ServiceBus;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class HttpTrigger1
    {
        private readonly ILogger _logger;

        public HttpTrigger1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HttpTrigger1>();
        }

        [Function("HttpTrigger1")]
        public async Task<OutputType> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req, FunctionContext context)
        {
            _logger.LogInformation($"C# HTTP trigger function processed a request for {context.InvocationId}.");

            HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync($"body of the HTTP response for {context.InvocationId}.");
            
            return new OutputType()
            {
                OutputEvent = $"body of the Service Bus response for {context.InvocationId}.",
                HttpResponse = response
            };
        }
    }
    public class OutputType
    {
        [ServiceBusOutput("outputQueue", Connection = "ServiceBusConnection")]
        public string OutputEvent { get; set; }

        public HttpResponseData HttpResponse { get; set; }
    }

}
