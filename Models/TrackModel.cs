using System.Collections.ObjectModel;

namespace AudioSphere.Models;

public class TrackModel : BaseViewModel
{
    private string _name;
    private string _color;
    private float _volume;
    private float _pan;
    private bool _isMuted;
    private bool _isSoloed;
    private bool _isRecordEnabled;
    private ObservableCollection<string>? _audioClips;

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }
    public string Color
    {
        get => _color;
        set => SetProperty(ref _color, value);
    }
    public float Volume
    {
        get => _volume;
        set => SetProperty(ref _volume, value);
    }
    public float Pan
    {
        get => _pan;
        set => SetProperty(ref _pan, value);
    }
    public bool IsMuted
    {
        get => _isMuted;
        set => SetProperty(ref _isMuted, value);
    }

    public bool IsSoloed
    {
        get => _isSoloed;
        set => SetProperty(ref _isSoloed, value);
    }
    public bool IsRecordEnabled
    {
        get => _isRecordEnabled;
        set => SetProperty(ref _isRecordEnabled, value);
    }
    public ObservableCollection<string> AudioClips
    {
        get => _audioClips!;
        set => SetProperty(ref _audioClips, value);
    }


}
