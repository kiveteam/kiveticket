using Autofac;
using System.Diagnostics;
using System.Text.RegularExpressions;
using TS.Bootstrapper;

namespace TS;

internal class Program : IDisposable
{
    private IContainer? _container;
    private IApplicationBootstrapper? _bootstrapper;
    private CancellationTokenSource _cts = new();

    public Program()
    {
        Console.CancelKeyPress += Process_Exited;
        AppDomain.CurrentDomain.ProcessExit += Process_Exited;
    }

    static void Main(string[] args)
    {
        using Program program = new();
        program.Start();
    }

    private void Start()
    {
        ContainerBuilder builder = new();
        builder.RegisterModule<Registration.RegistrationModule>();
        _container = builder.Build();
        _bootstrapper = _container.Resolve<IApplicationBootstrapper>();
        IApplication application = _bootstrapper?.CreateApplication() 
            ?? throw new InvalidOperationException("Приложение не инициализировано");
        application.Run();
        Console.WriteLine("Bot is running");
        while (application.IsStarted)
        {
            string command = Console.ReadLine() ?? string.Empty;
            switch (command)
            {
                case "bot stop":
                case "bot -s":
                case "bot -c":
                    Console.WriteLine("Bot stop in process");
                    application.Close();
                    break;
                case "bot -h":
                    Console.WriteLine(
                        """
                        KIVETicket system bot HELP
                        ======================
                        bot -h: Help bot
                        bot [-c, -s, stop]: Stop bot
                        """);
                    break;
                default:
                    Console.WriteLine($"Unknown bot command \"{command}\"");
                    break;

            }
        }
        Console.WriteLine("Bot was stopped");
    }

    private void Process_Exited(object? sender, EventArgs e)
    {
        Console.WriteLine("Bot stop in process");
        Dispose();
        Console.WriteLine("Bot was stopped");
        Environment.Exit(0);
    }

    public void Dispose()
    {
        _container?.Dispose();
        GC.SuppressFinalize(this);
    }
}
