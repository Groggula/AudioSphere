using AudioSphere.Helpers;
using AudioSphere.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Input;

namespace AudioSphere.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private AudioRecorder _recorder;
    private bool _isRecording;

    public event PropertyChangedEventHandler? PropertyChanged;
    public string RecordButtonColor => IsRecording ? "Black" : "Red";

    public bool IsRecording
    {
        get => _isRecording;
        set
        {
            if (_isRecording != value)
            {
                _isRecording = value;
                OnPropertyChanged(nameof(IsRecording));
                OnPropertyChanged(nameof(RecordButtonColor));
                RecordCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public RelayCommand RecordCommand { get; }
    public ICommand StartRecordingCommand { get; }
    public ICommand StopRecordingCommand { get; }

    public MainViewModel()
    {
        _recorder = new AudioRecorder();
        RecordCommand = new RelayCommand(ToggleRecording, () => true);
        StartRecordingCommand = new RelayCommand(StartRecording, () => !IsRecording);
        StopRecordingCommand = new RelayCommand(StopRecording, () => IsRecording);
    }

    private void ToggleRecording()
    {
        if (IsRecording)
        {
            StopRecording();
        }
        else
        {
            StartRecording();
        }
    }

    private void StartRecording()
    {
        string outputFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AudioSphere_Recording.wav");
        int selectedDeviceInputIndex = Settings.Default.AudioInputDeviceIndex;
        _recorder.StartRecording(outputFilePath, selectedDeviceInputIndex);
        IsRecording = true;
    }

    private void StopRecording()
    {
        _recorder.StopRecording();
        IsRecording = false;
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
