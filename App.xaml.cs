using AudioSphere.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace AudioSphere;


public partial class App : Application
{
    private IServiceProvider _serviceProvider;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        ConfigureServices();

        if (_serviceProvider == null)
        {
            throw new InvalidOperationException("ServiceProvider is not initialized.");
        }

        var mainwindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainwindow.Show();
    }

    private void ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<MainViewModel>();
        services.AddTransient<AddNewTrackViewModel>();
        services.AddTransient<SettingsViewModel>();
        services.AddSingleton<TimelineViewModel>();

        services.AddSingleton<MainWindow>();

        _serviceProvider = services.BuildServiceProvider();
    }
}
