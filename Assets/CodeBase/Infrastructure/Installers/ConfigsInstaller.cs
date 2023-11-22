using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    public class ConfigsInstaller : MonoInstaller
    {
        [Header("Player"), SerializeField]
        private PlayerConfig _playerConfig;

        [Header("Weapons")]

        [SerializeField]
        private ProjectilePoolerConfig _projectileHolderConfig;

        [SerializeField]
        private ScreenPointAttackConfig _screenPointAttackConfig;

        public override void InstallBindings()
        {
            BindPlayerConfig();
            BindProjectilePoolerConfig();
            BindScreenPointAttackConfig();
        }

        private void BindPlayerConfig()
        {
            Container
                .Bind<PlayerConfig>()
                .FromInstance(_playerConfig)
                .AsSingle();
        }

        private void BindProjectilePoolerConfig()
        {
            Container
                .Bind<ProjectilePoolerConfig>()
                .FromInstance(_projectileHolderConfig)
                .AsSingle();
        }

        private void BindScreenPointAttackConfig()
        {
            Container
                .Bind<ScreenPointAttackConfig>()
                .FromInstance(_screenPointAttackConfig)
                .AsSingle();
        }
    }
}
