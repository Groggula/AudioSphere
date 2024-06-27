using NAudio.Wave;

namespace AudioSphere.Services;

public class AudioPlayback
{
    private WaveOutEvent _waveOut;
    private AudioFileReader _audioFileReader;

    public void StartAudioPlayback(string filePath)
    {
        _waveOut = new WaveOutEvent();
        _audioFileReader = new AudioFileReader(filePath);
        _waveOut.Init(_audioFileReader);
        _waveOut.Play();
    }

    public void StopAudioPlayback()
    {
        _waveOut.Stop();
    }
}
