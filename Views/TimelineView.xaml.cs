using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace AudioSphere.Views;

public partial class TimelineView : UserControl
{
    private bool _isUserScrollingSlider;

    public TimelineView()
    {
        InitializeComponent();
        ScrollSlider.ValueChanged += ScrollSlider_ValueChanged;
        TimelineScrollViewer.ScrollChanged += TimelineScrollViewer_ScrollChanged;
        ScrollSlider.AddHandler(Thumb.DragStartedEvent, new DragStartedEventHandler(SliderDragStarted));
        ScrollSlider.AddHandler(Thumb.DragCompletedEvent, new DragCompletedEventHandler(SliderDragCompleted));
    }

    private void SliderDragStarted(object sender, DragStartedEventArgs e)
    {
        _isUserScrollingSlider = true;
    }

    private void SliderDragCompleted(object sender, DragCompletedEventArgs e)
    {
        _isUserScrollingSlider = false;
    }

    private void ScrollSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (_isUserScrollingSlider || e.OldValue != e.NewValue)
        {
            TimelineScrollViewer.ScrollToHorizontalOffset(ScrollSlider.Value);
        }
    }

    private void TimelineScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        if (!_isUserScrollingSlider)
        {
            ScrollSlider.Value = e.HorizontalOffset;
        }
    }

    private void TimelineScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
        {
            double offset = TimelineScrollViewer.HorizontalOffset - e.Delta;

            if (offset < 0)
            {
                offset = 0;
            }
            else if (offset > TimelineScrollViewer.ScrollableWidth)
            {
                offset = TimelineScrollViewer.ScrollableWidth;
            }

            TimelineScrollViewer.ScrollToHorizontalOffset(offset);

            ScrollSlider.Value = TimelineScrollViewer.HorizontalOffset;

            e.Handled = true;
        }
    }
}
