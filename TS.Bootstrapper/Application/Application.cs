using Autofac;
using Autofac.Core;
using NLog;
using Telegram.Bot;
using TS.Bootstrapper.Factories;
using TS.Domain;
using TS.Domain.Factories;

namespace TS.Bootstrapper;

internal class Application : IApplication, IDisposable
{
    private const string _apiKey = @"7540641862:AAHALgGrMfUOYtWvqXs0tS_qb17PLJKljec";
    private static readonly ILogger _logger = LogManager.GetLogger(nameof(Application));
    private readonly ILifetimeScope _applicationLifetimeScope;
    private IBotEngine? _botEngine;
    private readonly CancellationTokenSource _cancellationTokenSource = new();

    public bool IsStarted { get; private set; }

    public Application(ILifetimeScope lifetimeScope)
    {
        _logger.Info("Created");
        _applicationLifetimeScope = lifetimeScope.BeginLifetimeScope(RegisterDependencies);
    }

    public Task Run()
    {
        InitializeDependencies();
        if (_botEngine == null)
            throw new InvalidOperationException("Ядро не инициализировано");
        IsStarted = true;
        return _botEngine.Run(_cancellationTokenSource.Token);
    }

    public void Close()
    {
        _cancellationTokenSource.Cancel();
        IsStarted = false;
    }

    private void RegisterDependencies(ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterModule<Infrastructure.Registration.RegistrationModule>();
        containerBuilder.RegisterType<TelegramBotClient>()
            .As<ITelegramBotClient>()
            .WithParameter("token", _apiKey)
            .SingleInstance();
        containerBuilder.RegisterGeneric(typeof(Factory<>))
            .As(typeof(IFactory<>))
            .SingleInstance();
    }

    private void InitializeDependencies()
    {
        _botEngine = _applicationLifetimeScope.Resolve<IBotEngine>();
    }

    public void Dispose()
    {
        Close();
        _cancellationTokenSource.Dispose();

        _applicationLifetimeScope.Dispose();

        _logger.Info("Disposed");
        GC.SuppressFinalize(this);
    }
}