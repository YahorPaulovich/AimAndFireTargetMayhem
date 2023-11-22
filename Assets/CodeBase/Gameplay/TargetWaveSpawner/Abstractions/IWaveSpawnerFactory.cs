using System;
using System.Collections.Generic;

public interface IWaveSpawnerFactory
{
    public IEnumerator<WaveSpawnerConfig> SpawnWaves(WaveSpawnerCatalogConfig config, TargetCollection targetCollection, PointsStorage pointsStorage, WaveExecutionProgress waveExecutionProgress);
    public void SpawnTargets(WaveSpawnerCatalogConfig config,  WaveSpawnerConfig waveConfig, TargetCollection targetCollection);
    public bool IsGameOver { get; }
    public event Action OnTargetSpawned;
    public event Action OnWaveSpawned;
    public event Action OnGameOver;
}
