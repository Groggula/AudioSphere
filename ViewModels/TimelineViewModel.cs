using AudioSphere.Models;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace AudioSphere.ViewModels;

public class TimelineViewModel : BaseViewModel
{
    public ObservableCollection<TimelineModel> Bars { get; set; }
	public ObservableCollection<TimelineModel> Beats { get; set; }

    public TimelineViewModel()
    {
		Bars = new ObservableCollection<TimelineModel>();
		for (int i = 1; i <= 100; i++)
		{
			Bars.Add(new TimelineModel { Beat = i });
		}
    }
}
