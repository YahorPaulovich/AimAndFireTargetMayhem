using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    public class WaveSpawnerInstaller : MonoInstaller, IInitializable
    {
        [SerializeField]
        private WaveSpawnerCatalogConfig _waveUpgradeCatalogConfig;

        [SerializeField]
        private List<Transform> _spawnPoints;

        public void Initialize()
        {
            _waveUpgradeCatalogConfig.SetSpawnPoints(_spawnPoints);
        }

        public override void InstallBindings()
        {
            BindWaveExecutionProgress();
            BindWaveSpawnerCatalogConfig();
            BindTargetCollection();
            BindInstallerInterfaces();
            BindTargetFactory();
        }

        private void BindWaveExecutionProgress()
        {
            Container
                .Bind<WaveExecutionProgress>()
                .AsSingle();
        }

        private void BindWaveSpawnerCatalogConfig()
        {
            Container
                .Bind<WaveSpawnerCatalogConfig>()
                .FromInstance(_waveUpgradeCatalogConfig)
                .AsSingle();
        }

        private void BindTargetCollection()
        {
            Container
                .Bind<TargetCollection>()
                .AsSingle();
        }

        private void BindInstallerInterfaces()
        {
            Container
                .BindInterfacesTo<WaveSpawnerInstaller>()
                .FromInstance(this)
                .AsSingle();
        }

        private void BindTargetFactory()
        {
            Container
                .Bind<IWaveSpawnerFactory>()
                .To<WaveSpawnerFactory>()
                .AsSingle();
        }
    }
}
