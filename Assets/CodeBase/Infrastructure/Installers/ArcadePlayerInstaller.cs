using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    public class ArcadePlayerInstaller : MonoInstaller
    {
        [SerializeField]
        private ArcadePlayerController _playerPrefab;

        [SerializeField]
        private Transform _playerSpawnPoint;

        public override void InstallBindings()
        {    
            BindArcadePlayer();
        }

        private void BindArcadePlayer()
        {
            ArcadePlayerController arcadePlayerController = Container
                            .InstantiatePrefabForComponent<ArcadePlayerController>(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity, null);

            Container
                .Bind<ArcadePlayerController>()
                .FromInstance(arcadePlayerController)
                .AsSingle();
        }
    }
}
