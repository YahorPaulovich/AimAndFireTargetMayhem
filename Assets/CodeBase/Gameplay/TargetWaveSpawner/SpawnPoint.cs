using UnityEngine;

public struct SpawnPoint
{
    public Transform Value { get; private set; }
    public bool IsBusy { get; private set; }

    public SpawnPoint(Transform value, bool isBusy)
    {
        Value = value;
        IsBusy = isBusy;
    }

    public void Set(Transform value, bool isBusy)
    {
        Value = value;
        IsBusy = isBusy;
    }

    public void SetBusy(bool isBusy)
    {
        IsBusy = isBusy;
    }
}
