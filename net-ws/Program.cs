using System.Net;
using CommandLine;
using Microsoft.Extensions.FileProviders;
using net_ws;

await Parser.Default.ParseArguments<CliOptions>(args)
    .WithParsedAsync(RunServerWithArgs);

static async Task RunServerWithArgs(CliOptions options)
{
    var builder = WebApplication.CreateSlimBuilder();

    builder.WebHost.ConfigureKestrel((_, serverOptions) =>
    {
        IPAddress ipAddress = options.External
            ? IPAddress.Any
            : IPAddress.Loopback;
        
        serverOptions.Listen(ipAddress, options.Port);
    });

    if (options.Silent)
    {
        builder.Logging.ClearProviders();
    }

    builder.Services.AddDirectoryBrowser();

    var app = builder.Build();

    var fileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory())
    {
        UsePollingFileWatcher = true
    };

    app.UseFileServer(new FileServerOptions()
    {
        FileProvider = fileProvider,
        EnableDirectoryBrowsing = true
    });

    await app.RunAsync();
}