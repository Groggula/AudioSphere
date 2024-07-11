namespace AudioSphere.Models;

public class TimeSignatureModel
{
    public int BeatsPerMeasure { get; set; }
    public int NoteValue { get; set; }

    public TimeSignatureModel(int beatsPerMeasure, int noteValue)
    {
        BeatsPerMeasure = beatsPerMeasure;
        NoteValue = noteValue;
    }

    public override string ToString()
    {
        return $"{BeatsPerMeasure}/{NoteValue}";
    }
}
