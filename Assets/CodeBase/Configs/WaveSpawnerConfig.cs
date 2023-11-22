using UnityEngine;

[CreateAssetMenu(
    fileName = "WaveSpawnerConfig",
    menuName = "Configs/WaveSpawner/New WaveSpawnerConfig")]
public sealed class WaveSpawnerConfig : ScriptableObject
{
    public string Id { get; internal set; }
    [field: SerializeField] public uint Threshold { get; private set; } = 10;
    [field: SerializeField, Min(0f)] public float Interval { get; private set; } = 0.1f;

    [SerializeField]
    private TargetConfig[] _configs;

    public TargetConfig[] GetAllTargets()
    {
        return _configs;
    }

    public TargetConfig FindTarget(string id)
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
}
