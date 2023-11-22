using Zenject;

public class WeaponsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindScreenPointToRayAttack();
        BindProjectilePooler();
    }

    private void BindScreenPointToRayAttack()
    {
        Container
            .Bind<ScreenPointToRayAttack>()
            .AsSingle();
    }

    private void BindProjectilePooler()
    {
        Container
            .Bind<ProjectilePooler>()
            .AsSingle();
    }
}
