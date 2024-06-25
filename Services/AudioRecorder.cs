using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Documents;
using NAudio.Wave;

namespace AudioSphere.Services;

public class AudioRecorder : IDisposable
{
    private WaveInEvent? _waveSource;
    private WaveFileWriter? _waveFile;

    public static List<string> GetAudioInputDevices()
    {
        var devices = new List<string>();
        for (int i = 0; i < WaveIn.DeviceCount; i++)
        {
            var capabilities = WaveIn.GetCapabilities(i);
            devices.Add(capabilities.ProductName);
        }
        return devices;
    }

    public static List<string> GetAudioOutputDevices()
    {
        var devices = new List<string>();
        for (int i = 0; i < WaveOut.DeviceCount ; i++)
        {
            var capabilities = WaveOut.GetCapabilities(i);
            devices.Add(capabilities.ProductName);
        }
        return devices;
    }

    public void StartRecording(string outputFilePath, int deviceIndex)
    {
        _waveSource = new WaveInEvent
        {
            DeviceNumber = deviceIndex,
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
