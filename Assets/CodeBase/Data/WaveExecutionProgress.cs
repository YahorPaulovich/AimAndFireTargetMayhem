using System;
using UnityEngine;

[Serializable]
public sealed class WaveExecutionProgress : IWaveExecutionProgress
{
    [SerializeField]
    private float _value = 0f;

    public float Value => _value;

    public void SetProgress(float value)
    {
        _value = value;
        OnProgressChanged?.Invoke(value);
    }

    public event Action<float> OnProgressChanged;
}
