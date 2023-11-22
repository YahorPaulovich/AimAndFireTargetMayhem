using System.Collections.Generic;
using UnityEngine;
using Zenject;

public sealed class UISystemViewAdapter : MonoContext
{
    [SerializeField] private PointsScorePanel _pointsView;
    private PointsPanelAdapter _pointsAdapter;

    [SerializeField] private WaveSpawnProgressPanel _spawnProgressView;
    private WaveSpawnProgressPanelAdapter _spawnProgressAdapter;

    [Inject]
    public void Construct(PointsStorage pointsStorage, WaveExecutionProgress waveExecutionProgress)
    {
        _pointsAdapter = new PointsPanelAdapter(_pointsView, pointsStorage);
        _spawnProgressAdapter = new WaveSpawnProgressPanelAdapter(_spawnProgressView, waveExecutionProgress);
    }

    protected override IEnumerable<object> ProvideComponents()
    {
        yield return _pointsAdapter;
        yield return _spawnProgressAdapter;
    }
}
