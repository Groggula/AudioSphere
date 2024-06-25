using AudioSphere.ViewModels;
using AudioSphere.Views;
using System.Windows;
using System.Windows.Input;

namespace AudioSphere;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnClose_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void OpenSettings_Click(object sender, RoutedEventArgs e)
    {
        var settingsWindow = new SettingsWindow
        {
            DataContext = new SettingsViewModel()
        };
        settingsWindow.ShowDialog();
    }
}
