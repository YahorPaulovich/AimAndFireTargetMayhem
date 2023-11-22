using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public sealed class WaveSpawnerFactory : IWaveSpawnerFactory
{
    private readonly DiContainer _diContainer;
    private bool _isGameOver = false;
    private float _timer = 0f;

    public bool IsGameOver => _isGameOver;

    public WaveSpawnerFactory(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    public IEnumerator<WaveSpawnerConfig> SpawnWaves(
     WaveSpawnerCatalogConfig config,
     TargetCollection targetCollection,
     PointsStorage pointsStorage,
     WaveExecutionProgress waveExecutionProgress)
    {
        _timer = 0f;
        _isGameOver = false;

        IEnumerator waveEnumerator = config.GetAllWaveSpawners().GetEnumerator();

        while (!_isGameOver && waveEnumerator.MoveNext())
        {
            WaveSpawnerConfig waveConfig = (WaveSpawnerConfig)waveEnumerator.Current;
            _timer = 0f;

            while (pointsStorage.Points < waveConfig.Threshold)
            {
                _timer += Time.deltaTime;
                waveExecutionProgress.SetProgress(_timer / waveConfig.Interval);

                if (_timer >= waveConfig.Interval)
                {
                    _timer = 0f;
                    SpawnTargets(config, waveConfig, targetCollection);
                }

                if (_isGameOver)
                {
                    _timer = 0f;
                    break;
                }

                yield return null;
            }
        }
    }

    public void SpawnTargets(WaveSpawnerCatalogConfig config, WaveSpawnerConfig waveConfig, TargetCollection targetCollection)
    {
        if (_isGameOver)
        {
            return;
        }

        uint occupiedPoints = 0;
        TargetConfig[] allTargets = waveConfig.GetAllTargets();

        for (int i = 0, j = 0; i < config.SpawnPoints.Length; i++, j++)
        {
            if (j >= allTargets.Length)
            {
                j = 0;
            }

            if (!config.SpawnPoints[i].IsBusy)
            {
                GameObject targetPrefab = allTargets[j].TargetPrefab;
                allTargets[j].SetSpawnPoint(config.SpawnPoints[i].Value);
                Vector3 spawnPosition = config.SpawnPoints[i].Value.position;

                GameObject targetObject = _diContainer.InstantiatePrefab(
                    targetPrefab, spawnPosition, Quaternion.identity, config.SpawnPoints[i].Value);

                targetObject.transform.localScale = allTargets[j].LocalScale;

                if (targetObject.TryGetComponent<Renderer>(out var renderer))
                {
                    Material material = renderer.material;
                    if (allTargets[j].RandomizeableColor)
                    {
                        material.color = allTargets[j].GetRandomizeColor();
                    }
                    else
                    {
                        material.color = allTargets[j].Color;
                    }
                }
                else
                {
                    Debug.LogWarning("Target object does not have a Renderer component to assign color!");
                }

                if (targetObject.TryGetComponent<Target>(out var target))
                {
                    target.SetImpactPrefab(allTargets[j].ImpactPrefab);
                    target.SetTargetType(allTargets[j].TargetType);
                    target.SetSpawnPointIndex((uint)i);
                }

                config.SpawnPoints[i].SetBusy(true);

                targetCollection.Add(target);

                OnTargetSpawned?.Invoke();

                if (i == config.SpawnPoints.Length - 1)
                {
                    OnWaveSpawned?.Invoke();
                }
            }
            else
            {
                occupiedPoints++;
            }

            if (occupiedPoints >= config.SpawnPoints.Length)
            {         
                targetCollection.Clear();
                _isGameOver = true;
                OnGameOver?.Invoke();
                break;
            }
        }
    }

    public event Action OnTargetSpawned;
    public event Action OnWaveSpawned;
    public event Action OnGameOver;
}
