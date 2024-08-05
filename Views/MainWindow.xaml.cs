using AudioSphere.ViewModels;
using AudioSphere.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace AudioSphere;

public partial class MainWindow : Window
{
    private MainViewModel _mainVM;
    private TimelineViewModel _timelineVM;
    public MainWindow(MainViewModel mainVM, TimelineViewModel timelineVM)
    {
        InitializeComponent();
        _mainVM = mainVM ?? throw new ArgumentNullException(nameof(mainVM));
        _timelineVM = timelineVM ?? throw new ArgumentNullException(nameof(timelineVM));

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
            DataContext = new SettingsViewModel(_mainVM)
        };
        settingsWindow.ShowDialog();
    }

    private void NewTrack_Click(object sender, RoutedEventArgs e)
    {
        var addNewTrackWindow = new AddNewTrackWindow
        {
            DataContext = new AddNewTrackViewModel(_timelineVM ,_mainVM)
        };
        addNewTrackWindow.ShowDialog();
    }
}
