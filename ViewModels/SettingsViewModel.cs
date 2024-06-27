using AudioSphere.Helpers;
using AudioSphere.Services;
using AudioSphere.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace AudioSphere.ViewModels;

public class SettingsViewModel : INotifyPropertyChanged
{
    private int _selectedInputDeviceIndex;
    private int _selectedOutputDeviceIndex;

    public ObservableCollection<string> AudioInputDevices { get; }
    public ObservableCollection<string> AudioOutputDevices { get; }

    public int SelectedInputDeviceIndex
    {
        get => _selectedInputDeviceIndex;
        set
        {
            if (_selectedInputDeviceIndex != value)
            {
                //_selectedInputDeviceIndex = value;
                Properties.Settings.Default.AudioInputDeviceIndex = value;
                OnPropertyChanged(nameof(SelectedInputDeviceIndex));
            }
        }
    }

    public int SelectedOutputDeviceIndex
    {
        get => _selectedOutputDeviceIndex;
        set 
        {
            if(_selectedOutputDeviceIndex != value)
            {
                //_selectedOutputDeviceIndex = value;
                Properties.Settings.Default.AudioOutputDeviceIndex = value;
                OnPropertyChanged(nameof(SelectedOutputDeviceIndex));
            }
        }
    }

    public ICommand SaveCommand { get; }
    public SettingsViewModel()
    {
        AudioInputDevices = new ObservableCollection<string>(AudioRecorder.GetAudioInputDevices());
        AudioOutputDevices = new ObservableCollection<string>(AudioRecorder.GetAudioOutputDevices());
        SaveCommand = new RelayCommand(SaveSettings);
    }

    private void SaveSettings()
    {
        Properties.Settings.Default.AudioInputDeviceIndex = SelectedInputDeviceIndex;
        Properties.Settings.Default.AudioOutputDeviceIndex = SelectedOutputDeviceIndex;
        Properties.Settings.Default.Save();
        Application.Current.Windows.OfType<SettingsWindow>().FirstOrDefault()?.Close();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
