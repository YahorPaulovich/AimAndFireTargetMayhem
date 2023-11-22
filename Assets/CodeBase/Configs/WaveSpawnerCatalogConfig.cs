using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "WaveSpawnerCatalogConfig",
    menuName = "Configs/WaveSpawner/New WaveSpawnerCatalogConfig")]
public sealed class WaveSpawnerCatalogConfig : ScriptableObject
{
    [HideInInspector]
    public SpawnPoint[] SpawnPoints { get; private set; }

    [SerializeField]
    private WaveSpawnerConfig[] _configs;

    public WaveSpawnerConfig[] GetAllWaveSpawners()
    {
        return _configs;
    }

    public WaveSpawnerConfig FindWaveSpawner(string id)
    {
        for (int i = 0; i < _configs.Length; i++)
        {
            var config = _configs[i];
            if (config.Id == id)
            {
                return config;
            }
        }

        return null;
    }

    public void SetSpawnPoints(List<Transform> spawnPoints)
    {
        int count = spawnPoints.Count;
        SpawnPoints = new SpawnPoint[count];
        for (int i = 0; i < count; i++)
        {
            SpawnPoints[i].Set(spawnPoints[i], false);
        }
    }
}
