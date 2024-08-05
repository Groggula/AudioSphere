using AudioSphere.Models;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace AudioSphere.ViewModels;

public class TimelineViewModel : BaseViewModel
{
    public ObservableCollection<TimelineModel> Bars { get; set; }
	public ObservableCollection<TimelineModel> Beats { get; set; }

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

    public TimelineViewModel()
    {
        _trackList = new ObservableCollection<TrackModel>();
        _newTrackItem = new TrackModel();


        Bars = new ObservableCollection<TimelineModel>();
		for (int i = 1; i <= 100; i++)
		{
			Bars.Add(new TimelineModel { Beat = i });
		}

        Beats = new ObservableCollection<TimelineModel>();
    }

    public void AddTrack()
    {
        if (NewTrackItem != null)
        {
            TrackList.Add(new TrackModel { Name = $"Audio {TrackList.Count + 1}", Color = "Red", Pan = 0 }); ;
        }
    }

    public void DeleteTrack(TrackModel track)
    {
        if (track != null && TrackList.Contains(track))
        {
            TrackList.Remove(track);
        }
    }
}
