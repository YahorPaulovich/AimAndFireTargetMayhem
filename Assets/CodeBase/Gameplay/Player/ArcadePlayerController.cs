using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(ArcadePlayerInputs))]
public class ArcadePlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private ArcadePlayerInputs _input;

    private IWeapon _weapon;
    private PlayerConfig _playerConfig;

    [Inject]
    private void Construct(PlayerConfig playerConfig)
    {
        _playerConfig = playerConfig;

        _playerInput = GetComponent<PlayerInput>();
        _input = GetComponent<ArcadePlayerInputs>();

        _weapon = GetComponent<ProjectilePooler>();//GetComponent<ScreenPointToRayAttack>();
    }

    private void OnValidate()
    {
        _playerInput ??= GetComponent<PlayerInput>();
        _input ??= GetComponent<ArcadePlayerInputs>();
    }

    private void OnEnable()
    {
        _input.OnFired += OnFired;
    }

    private void OnDisable()
    {
        _input.OnFired -= OnFired;
    }

    private void OnFired()
    {
        PerformGunshot();
    }

    private void PerformGunshot()
    {
        var screenPostion = _input.ScreenPostion;
        var camera = _input.Camera;

        _weapon.PerformGunshot(camera, screenPostion);
    }
}
