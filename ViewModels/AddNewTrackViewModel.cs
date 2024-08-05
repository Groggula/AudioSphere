using AudioSphere.Helpers;
using AudioSphere.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace AudioSphere.ViewModels;

public class AddNewTrackViewModel : BaseViewModel
{
    private MainViewModel _mainVM;
    private TimelineViewModel _timelineVM;
    private TrackModel _trackItem;

    public TrackModel TrackItem
    {
        get => _trackItem;
        set => SetProperty(ref _trackItem, value);
    }

    public ICommand AddTrackCommand { get; }
    public ICommand CancelAddTrackCommand { get; }

    public AddNewTrackViewModel(TimelineViewModel timelineVM, MainViewModel mainVM)
    {
        _mainVM = mainVM ?? throw new ArgumentNullException(nameof(mainVM));
        _timelineVM = timelineVM ?? throw new ArgumentNullException(nameof(timelineVM));

        _trackItem = new TrackModel();

        AddTrackCommand = new RelayCommand(_ => AddTrack(), _ => true);
        CancelAddTrackCommand = new RelayCommand(_ => CancelAddTrack(), _ => true);

        _trackItem.SelectableColors = new ObservableCollection<string> { "Blue", "Green", "Red", "Yellow" };
    }

    private void AddTrack()
    {
        if (TrackItem != null)
        {
            _timelineVM.TrackList.Add(new TrackModel { Name = TrackItem.Name, Color = TrackItem.Color, Pan = 5 });
        }
    }

    private void CancelAddTrack()
    {
        foreach (Window window in Application.Current.Windows)
        {
            if (window.DataContext == this)
            {
                window.Close();
            }
        }
    }
}