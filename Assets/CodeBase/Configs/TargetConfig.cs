using UnityEngine;

[CreateAssetMenu(
    fileName = "TargetConfig",
    menuName = "Configs/WaveSpawner/New TargetConfig")]
public sealed class TargetConfig : ScriptableObject
{
    private void OnValidate()
    {
        ValidateTargetPrefab();
        ValidateImpactPrefab();
    }

    private void ValidateTargetPrefab()
    {
        if (!TargetPrefab)
        {
            Debug.LogWarning("Target object prefab not defined!");
        }
        else
        {
            var scale = Vector3.one;
            switch (TargetType)
            {
                case TargetType.Big:
                    scale = new Vector3(1.5f, 1.5f, 1.5f);
                    break;
                case TargetType.Medium:
                    break;
                case TargetType.Small:
                    scale = new Vector3(0.5f, 0.5f, 0.5f);
                    break;
            }

            LocalScale = scale;
        }
    }

    private void ValidateImpactPrefab()
    {
        if (!ImpactPrefab)
        {
            Debug.LogWarning("Impact object prefab not defined!");
        }
    }

    public string Id { get; internal set; }
    [field: SerializeField] public GameObject TargetPrefab { get; private set; }
    public Transform SpawnPoint { get; private set; }
    public Vector3 LocalScale { get; private set; }
    [field: SerializeField] public TargetType TargetType { get; private set; }
    [field: SerializeField] public Color Color { get; private set; }
    [field: SerializeField] public bool RandomizeableColor { get; private set; } = true;
    [field: SerializeField] public ParticleSystem ImpactPrefab { get; private set; }
    [field: SerializeField, Min(0f)] public float ParticleSystemDuration { get; private set; } = 2f;

    public Color GetRandomizeColor()
    {
        Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
        return color;
    }

    public void SetSpawnPoint(Transform spawnPoint)
    {
        SpawnPoint = spawnPoint;
    }
}
