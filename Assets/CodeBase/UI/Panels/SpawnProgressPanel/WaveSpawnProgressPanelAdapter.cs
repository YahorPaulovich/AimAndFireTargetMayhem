using UnityEngine;

public sealed class WaveSpawnProgressPanelAdapter : IEnableComponent, IDisableComponent
{
    [SerializeField]
    private WaveSpawnProgressPanel _view;
    private WaveExecutionProgress _progress;

    public WaveSpawnProgressPanelAdapter(WaveSpawnProgressPanel waveSpawnProgressPanel, WaveExecutionProgress waveExecutionProgress)
    {
        _view = waveSpawnProgressPanel;
        _progress = waveExecutionProgress;
    }

    private void OnProgressChanged(float value)
    {
        _view.UpdateProgressUI(value);
    }

    void IEnableComponent.OnEnable()
    {
        _progress.OnProgressChanged += OnProgressChanged;
        _view.UpdateProgressUI(_progress.Value);
    }

    void IDisableComponent.OnDisable()
    {
        _progress.OnProgressChanged -= OnProgressChanged;
    }
}
