using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    public class PointsSystemInstaller : MonoInstaller
    {
        [SerializeField]
        private PointsStorage _pointsStorage;

        public override void InstallBindings()
        {
            Container
                .Bind<PointsStorage>()
                .FromInstance(_pointsStorage)
                .AsSingle();
        }
    }
}
