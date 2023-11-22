using System;
using UnityEngine;
using Zenject;

public sealed class Target : MonoBehaviour, ITarget, IDamageable
{
    [SerializeField]
    private float _particleSystemDuration = 2f;

    private TargetType _targetType;
    private ParticleSystem _impactPrefab;
    private uint _spawnPointIndex;

    private WaveSpawnerCatalogConfig _waveSpawnerCatalogConfig;
    private PointsStorage _pointStorage;    

    [Inject]
    private void Construct(WaveSpawnerCatalogConfig waveSpawnerCatalogConfig, PointsStorage pointsStorage)
    {
        _waveSpawnerCatalogConfig = waveSpawnerCatalogConfig;
        _pointStorage = pointsStorage;
    }

    public void SetTargetType(TargetType targetType)
    {
        _targetType = targetType;
    }

    public void SetImpactPrefab(ParticleSystem particleSystem)
    {
        _impactPrefab = particleSystem;
    }

    public uint SpawnPointIndex => _spawnPointIndex;

    public void SetSpawnPointIndex(uint spawnPointIndex)
    {
        _spawnPointIndex = spawnPointIndex;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<Projectile>(out var projectile))
        {
            OnHited?.Invoke(_targetType);
            ApplyRewardForPlayer();
            ApplyDamage();
        }
    }

    private void ReleasePoint()
    {
        _waveSpawnerCatalogConfig.SpawnPoints[_spawnPointIndex].SetBusy(false);
    }

    private void CreateParticles(float delay)
    {
        var rotation = new Quaternion(90f, 0f, 0f, 90f);
        var particles = Instantiate(_impactPrefab, transform.position, rotation);
        var particleMain = particles.main;
        var renderer = GetComponent<Renderer>();
        particleMain.startColor = new ParticleSystem.MinMaxGradient(renderer.material.color);
        particles.Play();

        Destroy(particles.gameObject, delay);
    }

    public void ApplyRewardForPlayer()
    {
        _pointStorage.AddPointsByTargetType(_targetType);
    }

    public void ApplyDamage()
    {
        ReleasePoint();
        CreateParticles(_particleSystemDuration);
        OnDestroyed?.Invoke(this);
        Destroy(gameObject);
    }

    public void Clear()
    {
        ApplyDamage();
    }

    public event Action<TargetType> OnHited;
    public event Action<Target> OnDestroyed;
}
