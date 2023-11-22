using UnityEngine;

[CreateAssetMenu(fileName = "ScreenPointAttackConfig", menuName = "Configs/Weapons/New ScreenPointAttackConfig")]
public class ScreenPointAttackConfig : ScriptableObject
{
    [field: SerializeField] public LayerMask LayerMask { get; private set; }
    [field: SerializeField, Min(0f)] public float Damage { get; private set; } = 10f;
}
