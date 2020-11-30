# Firebus

An easy way to perform fire-and-forget, delayed jobs inside ASP.Net Core applications inspired by
[Hangfire](https://github.com/HangfireIO/Hangfire). Firebus uses queueing service like Azure Service Bus.

## Firebus.AzureServiceBus

Use Azure Service Bus queue as queuing service of Firebus.

### Usage

#### Installation
```
Install-Package Firebus.AzureServiceBus
```
`Firebus.AzureServiceBus` package is depends on `Firebus` package and the depended package will installed automatially.

```C#
public void ConfigureServices(IServiceCollection services)
{
    ...

    services.AddFirebusServer(options =>
    {
        options.UseAzureServiceBus(new AzureServiceBusServerOptions
            {
                ConnectionString = Configuration.GetConnectionString("AzureServiceBus"),
                QueueNames = new[] {"default", "myqueue"} // Listens form multiple queues
            })
            .AddBeforeExecuteJobFilter<BeforeExecuteJobFilter>();
    });
    services.AddFirebusClient(options =>
    {
        options.UseAzureServiceBus(new AzureServiceBusClientOptions
            {
                ConnectionString = Configuration.GetConnectionString("AzureServiceBus"),
                DefaultQueueName = "default"
            })
            .AddBeforeRegisterJobFilter<BeforeRegisterJobFilter>();
    });
}

...

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    ...

    app.UseFirebusServer();
}
```

#### Register background job
```C#
public class SampleController : Controller
{
    private readonly FirebusClient _firebusClient;

    ...

    public SampleController(FirebusClient firebusClient)
    {
        // Injected as a service
        _firebusClient = firebusClient;
    }

    ...

    public async Task<IActionResult> Enqueue()
    {
        await _firebusClint.RegisterJobAsync<SampleService>(svc => svc.FooBar("baz")); // Simple fire-and-forget background job
        await _firebusClint.RegisterJobAsync<SampleService>(svc => svc.FooBar("baz"), DateTime.UtcNow.AddHours(1)); // Fires after 1 hours from now
        await _firebusClint.RegisterJobInQueueAsync<SampleService>(svc => svc.FooBar("baz"), "myqueue"); // Use qeueue named "myqueue"
    }
}
```
