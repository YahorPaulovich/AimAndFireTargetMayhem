using System;

public interface IWaveExecutionProgress
{
    public float Value { get; }
    public void SetProgress(float value);
    public event Action<float> OnProgressChanged;
}
