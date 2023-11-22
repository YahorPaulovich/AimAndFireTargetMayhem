using UnityEngine;
using Zenject;

public class ScreenPointToRayAttack : MonoBehaviour, IWeapon
{
    private ScreenPointAttackConfig _screenPointAttackConfig;

    [Inject]
    private void Construct(ScreenPointAttackConfig screenPointAttackConfig)
    {
        _screenPointAttackConfig = screenPointAttackConfig;
    }

    public void PerformGunshot(Camera camera, Vector2 screenPostion)
    {
        Ray ray = camera.ScreenPointToRay(screenPostion);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _screenPointAttackConfig.LayerMask))
        {
            if (hitInfo.collider.TryGetComponent(out ITarget target))
            {
                target.ApplyRewardForPlayer();
            }
            if (hitInfo.collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage(/*_screenPointAttackConfig.Damage*/);
            }
        }
    }
}
