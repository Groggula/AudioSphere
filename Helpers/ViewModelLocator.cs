using AudioSphere.ViewModels;

namespace AudioSphere.Helpers
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel { get; }
        public TimelineViewModel TimelineViewModel { get; }

        public ViewModelLocator()
        {
            TimelineViewModel = new TimelineViewModel();
            MainViewModel = new MainViewModel(TimelineViewModel);
        }
    }
}