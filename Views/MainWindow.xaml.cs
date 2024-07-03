using AudioSphere.ViewModels;
using AudioSphere.Views;
using System.Windows;
using System.Windows.Input;

namespace AudioSphere;

public partial class MainWindow : Window
{
    private MainViewModel _mainVM;
    public MainWindow()
    {
        InitializeComponent();
        _mainVM = new MainViewModel();
        DataContext = _mainVM;
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

    private void NewTrack_Click(object sender, RoutedEventArgs e)
    {
        var addNewTrackWindow = new AddNewTrackWindow
        {
            DataContext = new AddNewTrackViewModel(_mainVM)
        };
        addNewTrackWindow.ShowDialog();
    }
}
