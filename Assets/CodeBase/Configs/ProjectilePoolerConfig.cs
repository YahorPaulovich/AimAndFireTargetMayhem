using UnityEngine;

[CreateAssetMenu(fileName = "ProjectilePoolerConfig", menuName = "Configs/Weapons/New ProjectilePoolerConfig")]
public class ProjectilePoolerConfig : ScriptableObject
{
    [field: SerializeField] public Projectile ProjectilePrefab { get; private set; }
    [field: SerializeField, Min(0f)] public float MuzzleVelocity { get; private set; } = 4500f;
    [field: SerializeField, Min(0f)] public float Offset { get; private set; } = 0.01f;
    [field: SerializeField, Min(0f)] public float CooldownWindow { get; private set; } = 0.1f;
    [field: SerializeField] public bool CollectionCheck { get; private set; } = true;
    [field: SerializeField, Range(5, 20)] public int DefaultCapacity { get; private set; } = 20;
    [field: SerializeField] public int MaxSize { get; private set; } = 100;
}
