using Amazon.SQS;
using AWS.Services.ElasticContainerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddDefaultAWSOptions(hostContext.Configuration.GetAWSOptions());
        services.AddAWSService<IAmazonSQS>();
        services.AddLogging(configure =>
        {
            configure.AddAWSProvider(hostContext.Configuration.GetAWSLoggingConfigSection());
        });
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
