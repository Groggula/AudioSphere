using AudioSphere.Helpers;
using AudioSphere.Models;
using AudioSphere.Services;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace AudioSphere.ViewModels;

public class MainViewModel : BaseViewModel
{
    private AudioRecorder _recorder;
    private TimerModel _timer;
    private bool _isRecording;

    public string RecordButtonColor => IsRecording ? "Black" : "Red";
    public string ElapsedTimeString => _timer.ElapsedTimeString;

    private ObservableCollection<TrackModel> _trackList;
    private TrackModel _newTrackItem;

    public TrackModel NewTrackItem
    {
        get => _newTrackItem;
        set => SetProperty(ref _newTrackItem, value);
    }


    public ObservableCollection<TrackModel> TrackList
    {
        get => _trackList;
        set => SetProperty(ref _trackList, value);
    }


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

    public ICommand AddTrackCommand { get; }
    public ICommand DeleteTrackCommand { get; }
    public ICommand ImportAudioFileCommand { get; }
    public RelayCommand RecordCommand { get; }
    public RelayCommand ToggleMuteTrackCommand { get; }
    //public ICommand StartRecordingCommand { get; }
    //public ICommand StopRecordingCommand { get; }

    public MainViewModel()
    {
        _recorder = new AudioRecorder();
        _timer = new TimerModel();
        _timer.PropertyChanged += TimerModel_PropertyChanged;
        _newTrackItem = new TrackModel();
        _trackList = new ObservableCollection<TrackModel>();

        AddTrackCommand = new RelayCommand(_ => AddNewTrack(), _ => true);
        RecordCommand = new RelayCommand(_ => ToggleRecording(), _ => true);
        ImportAudioFileCommand = new RelayCommand(_ =>  ImportAudioFile(), _ => true);
        ToggleMuteTrackCommand = new RelayCommand(_ => ToggleMuteTrack(), _ => true);
        DeleteTrackCommand = new RelayCommand(param => DeleteTrack(param as TrackModel), param => param is TrackModel);
        //StartRecordingCommand = new RelayCommand(StartRecording, () => !IsRecording);
        //StopRecordingCommand = new RelayCommand(StopRecording, () => IsRecording);


    }

    private void ToggleMuteTrack()
    {
        //
    }

    private void AddNewTrack()
    {
        if (NewTrackItem != null)
        {
            TrackList.Add(new TrackModel { Name = $"Audio {TrackList.Count +1}", Color = "Red", Pan = 0 }); ;
        }
    }

    private void DeleteTrack(TrackModel track)
    {
        if (track != null && TrackList.Contains(track))
        {
            TrackList.Remove(track);
        }
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
        int selectedDeviceInputIndex = Properties.Settings.Default.AudioInputDeviceIndex;
        _recorder.StartRecording(outputFilePath, selectedDeviceInputIndex);
        _timer.Start();
        IsRecording = true;
    }

    private void StopRecording()
    {
        _recorder.StopRecording();
        _timer.Stop();
        IsRecording = false;
    }

    private void ImportAudioFile()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Title = "Import audio file",
        };

        if (openFileDialog.ShowDialog() == true)
        {
            string filePath = openFileDialog.FileName;
            string fileName = Path.GetFileName(filePath);
            try
            {
                if (File.Exists(filePath))
                {
                    TrackList.Add(new TrackModel { Name = fileName, Color = "Transparent", Pan = 0 }); ;
                }
                else
                {
                    MessageBox.Show("Could not import file");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Audio file import failed: {ex.Message}");
            }
        }
    }

    private void TimerModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(TimerModel.ElapsedTimeString))
        {
            OnPropertyChanged(nameof(ElapsedTimeString));
        }
    }
}
