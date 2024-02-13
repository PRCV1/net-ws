using Microsoft.Extensions.FileProviders;

foreach (var arg in args)
{
    Console.WriteLine(arg);
}

Console.WriteLine(Directory.GetCurrentDirectory());

var builder = WebApplication.CreateSlimBuilder();

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