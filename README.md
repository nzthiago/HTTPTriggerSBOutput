# Azure Functions - .NET Isolated with output service bus examples

This simple repo has two options for sending out messages to Service Bus from a .NET Isolated function.

* [HttpTrigger1](./HttpTrigger1.cs) has both the HTTP response and the ServiceBusOutput binding; each with a different body content.
* [HttpTrigger2](./HttpTrigger2.cs) has the HTTP response and uses dependency injection to get a service bus client that is used to send  the message to service bus.

When testing locally, make sure to update [local.settings.json](local.settings.json) with the actual connection string to your Service Bus instance, and ensure there's a queue in it called `outputQueue`. If testing with a deployed app, ensure there's an app setting called `ServiceBusConnection` with the service bus connection string.
