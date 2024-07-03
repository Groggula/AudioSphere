using AudioSphere.Helpers;
using AudioSphere.Models;
using AudioSphere.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
        DeleteTrackCommand = new RelayCommand(param => DeleteTrack(param as TrackModel), param => param is TrackModel);
        RecordCommand = new RelayCommand(_ => ToggleRecording(), _ => true);
        //StartRecordingCommand = new RelayCommand(StartRecording, () => !IsRecording);
        //StopRecordingCommand = new RelayCommand(StopRecording, () => IsRecording);

        ToggleMuteTrackCommand = new RelayCommand(_ => ToggleMuteTrack(), _ => true);

    }

    private void ToggleMuteTrack()
    {
        //
    }

    private void AddNewTrack()
    {
        if (NewTrackItem != null)
        {
            TrackList.Add(new TrackModel { Name = "Audio 1", Color = "Red", Pan = 0 }); ;
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

    private void TimerModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(TimerModel.ElapsedTimeString))
        {
            OnPropertyChanged(nameof(ElapsedTimeString));
        }
    }
}
