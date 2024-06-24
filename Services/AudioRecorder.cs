using System;
using System.IO;
using NAudio.Wave;

namespace AudioSphere.Services;

public class AudioRecorder : IDisposable
{
    private WaveInEvent? _waveSource;
    private WaveFileWriter? _waveFile;

    public void StartRecording(string outputFilePath)
    {
        _waveSource = new WaveInEvent
        {
            WaveFormat = new WaveFormat(44100, 1)
        };

        _waveSource.DataAvailable += OnDataAvailable;
        _waveFile = new WaveFileWriter(outputFilePath, _waveSource.WaveFormat);

        _waveSource.StartRecording();
    }

    private void OnDataAvailable(object sender, WaveInEventArgs e)
    {
        if (_waveFile != null)
        {
            _waveFile.Write(e.Buffer, 0, e.BytesRecorded);
            _waveFile.Flush();
        }
    }

    public void StopRecording()
    {
        if (_waveSource != null)
        {
            _waveSource.StopRecording();
            _waveSource.Dispose();
            _waveSource = null;
        }

        if (_waveFile != null)
        {
            _waveFile.Dispose();
            _waveFile = null;
        }
    }

    public void Dispose()
    {
        StopRecording();
    }
}
