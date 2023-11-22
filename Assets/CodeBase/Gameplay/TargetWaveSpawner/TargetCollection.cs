using System;
using System.Collections.Generic;

public sealed class TargetCollection
{
    private List<Target> _targetList = new List<Target>();
    public event Action<Target> TargetDestroyed;

    public void Add(Target target)
    {
        _targetList.Add(target);
    }

    public void Remove(Target target)
    {
        _targetList.Remove(target);
        TargetDestroyed?.Invoke(target);
    }

    public void Clear()
    {
        foreach (var target in _targetList)
        {
            if (target != null)
            {
                target.Clear();
            }
        }

        _targetList.Clear();
    }
}
