using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace AudioSphere.Models;

public class TimerModel : BaseViewModel
{
	public string ElapsedTimeString => ElapsedTime.ToString(@"hh\:mm\:ss\.ff");

    private DispatcherTimer _timer;
	private TimeSpan _elapsedTime;

	public TimeSpan ElapsedTime
	{
		get => _elapsedTime;
		private set 
		{
			_elapsedTime = value;
			OnPropertyChanged(nameof(ElapsedTime));
			OnPropertyChanged(nameof(ElapsedTimeString));
		}
	}

    public TimerModel()
    {
        _timer = new DispatcherTimer();
		_timer.Interval = TimeSpan.FromMilliseconds(1000);
		_timer.Tick += Timer_Tick;
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        ElapsedTime = ElapsedTime.Add(TimeSpan.FromMilliseconds(1000));
    }

	public void Start()
	{
		ElapsedTime = TimeSpan.Zero;
		_timer.Start();
	}

	public void Stop()
	{
		_timer.Stop();
	}
}
