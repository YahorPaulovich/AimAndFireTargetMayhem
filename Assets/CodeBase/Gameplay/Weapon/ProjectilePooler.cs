using UnityEngine;
using UnityEngine.Pool;
using Zenject;

public sealed class ProjectilePooler : MonoBehaviour, IWeapon
{
    private Projectile _bulletPrefab;
    private float _muzzleVelocity;
    private float _offset;
    private float _cooldownWindow;

    private IObjectPool<Projectile> _objectPool;

    private bool _collectionCheck;

    private int _defaultCapacity;
    private int _maxSize;

    private float _nextTimeToShoot;

    private ProjectilePoolerConfig _projectilePoolerConfig;

    [Inject]
    private void Construct(ProjectilePoolerConfig projectileHolderConfig)
    {
        _projectilePoolerConfig = projectileHolderConfig;
        
        InitializePool();
    }

    private void InitializePool()
    {
        SetProjectilePooler(_projectilePoolerConfig);

        _objectPool = new ObjectPool<Projectile>(CreateBullet,
                    OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
                    _collectionCheck, _defaultCapacity, _maxSize);    
    }

    private Projectile CreateBullet()
    {
        Projectile projectileInstance = Instantiate(_bulletPrefab);
        projectileInstance.SetObjectPool = _objectPool;
        return projectileInstance;
    }

    private void OnReleaseToPool(Projectile pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    private void OnGetFromPool(Projectile pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    private void OnDestroyPooledObject(Projectile pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }

    private void SetProjectilePooler(ProjectilePoolerConfig config)
    {
        if (config.ProjectilePrefab == null)
        {
            Debug.LogError("Projectile object prefab not defined!");
            return;
        }
        else
        {
            _bulletPrefab = config.ProjectilePrefab;
            _muzzleVelocity = config.MuzzleVelocity;
            _offset = config.Offset;
            _cooldownWindow = config.CooldownWindow;
            _collectionCheck = config.CollectionCheck;
            _defaultCapacity = config.DefaultCapacity;
            _maxSize = config.MaxSize;

            _nextTimeToShoot = -_cooldownWindow;
        }
    }
    
    private void CalculateGunshot(Camera camera, Vector2 screenPostion, Projectile bulletObject, float muzzleVelocity)
    {
        Vector3 position;
        Vector3 direction;
        Vector3 velocity;

        var ray = camera.ScreenPointToRay(screenPostion);

        position = ray.origin;
        direction = ray.direction;
        velocity = direction * muzzleVelocity;

        bulletObject.transform.SetPositionAndRotation(position + direction * _offset, Quaternion.LookRotation(direction, Vector3.up));
        bulletObject.SetVelocity(velocity);
        bulletObject.Deactivate();
    }

    public void PerformGunshot(Camera camera, Vector2 screenPostion)
    {
        if (Time.time > _nextTimeToShoot && _objectPool != null)
        {
            Projectile bulletObject = _objectPool.Get();

            if (bulletObject == null)
            {
                return;
            }

            CalculateGunshot(camera, screenPostion, bulletObject, _muzzleVelocity);

            _nextTimeToShoot = Time.time + _cooldownWindow;
        }
    }
}
