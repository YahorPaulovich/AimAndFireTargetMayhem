using System;

public interface ITarget
{
    public void ApplyRewardForPlayer();
    public event Action<TargetType> OnHited;
    public event Action<Target> OnDestroyed;
}
