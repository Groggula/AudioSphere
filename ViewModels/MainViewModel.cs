using AudioSphere.Helpers;
using AudioSphere.Models;
using AudioSphere.Services;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace AudioSphere.ViewModels;

public class MainViewModel : BaseViewModel
{
    private AudioRecorder _recorder;
    private AudioPlayback _playback;
    private TimerModel _timer;
    private TimelineViewModel _timelineVM;
    private bool _isRecording;
    private bool _isPlaybackActive;

    public TimelineViewModel TimelineVM
    {
        get => _timelineVM;
        set => SetProperty(ref _timelineVM, value);
    }

    public string RecordButtonColor => IsRecording ? "Black" : "Red";
    public string ActiveButtonColor => IsPlaybackActive ? "Blue" : "#FFDADADA";
    public string ElapsedTimeString => _timer.ElapsedTimeString;

    public bool IsPlaybackActive
    {
        get => _isPlaybackActive;
        set
        {
            if (_isPlaybackActive != value)
            {
                _isPlaybackActive = value;
                OnPropertyChanged(nameof(IsPlaybackActive));
                OnPropertyChanged(nameof(ActiveButtonColor));
            }
        }
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
    public ICommand PlayCommand { get; }
    public ICommand PauseCommand { get; }
    public RelayCommand RecordCommand { get; }
    public RelayCommand ToggleMuteTrackCommand { get; }

    public MainViewModel(TimelineViewModel timelineVM)
    {
        _recorder = new AudioRecorder();
        _playback = new AudioPlayback();
        _timer = new TimerModel();
        _timer.PropertyChanged += TimerModel_PropertyChanged;

        PlayCommand = new RelayCommand(_ => Play(), _ => true);
        PauseCommand = new RelayCommand(_ => Pause(), _ => true);
        AddTrackCommand = new RelayCommand(_ => AddNewTrack(), _ => true);
        RecordCommand = new RelayCommand(_ => ToggleRecording(), _ => true);
        ImportAudioFileCommand = new RelayCommand(_ => ImportAudioFile(), _ => true);
        ToggleMuteTrackCommand = new RelayCommand(_ => ToggleMuteTrack(), _ => true);
        DeleteTrackCommand = new RelayCommand(param => DeleteTrack(param as TrackModel), param => param is TrackModel);

        _timelineVM = timelineVM ?? throw new ArgumentNullException(nameof(timelineVM));
    }
    public MainViewModel()
    {
        
    }

    private void Play()
    {
        IsPlaybackActive = true;
        _timer.Start();
    }

    private void Pause()
    {
        IsPlaybackActive = false;
        _timer.Stop();
    }

    private void ToggleMuteTrack()
    {
        //
    }

    private void AddNewTrack()
    {
        _timelineVM.AddTrack();
    }

    private void DeleteTrack(TrackModel track)
    {
        _timelineVM.DeleteTrack(track);
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
                    _timelineVM.TrackList.Add(new TrackModel { Name = fileName, Color = "Transparent", Pan = 0 }); ;
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
