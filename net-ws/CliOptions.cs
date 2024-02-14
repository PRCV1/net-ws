using CommandLine;

namespace net_ws;

public class CliOptions
{
    [Option('p', "port", Required = false, HelpText = "The port the webserver is running on.", Default = 8080)]
    public int Port { get; set; }

    [Option('e', "external", Required = false, HelpText = "Allows the webserver the be accesible from other devices", Default = false)]
    public bool External { get; set; }

    [Option('s', "silent", Required = false, HelpText = "Suppress log messages from output", Default = false)]
    public bool Silent { get; set; }
}