using System;
using UnityEngine;

[Serializable]
public sealed class PointsStorage
{
    [SerializeField]
    private uint _points = 0;

    [SerializeField]
    private uint _maxPoints = 10000;

    public uint Points => _points;
    public uint MaxPoints => _maxPoints;

    public void SetupPointsCapacity(uint pointsMax)
    {
        _maxPoints = pointsMax;
    }

    public void SetupPoints(uint points)
    {
        _points = points;
        OnPointsChanged?.Invoke(_points);
    }

    public void AddPoints(uint range)
    {
        _points += range;
        OnPointsChanged?.Invoke(_points);
    }

    public void AddPointsByTargetType(TargetType target)
    {
        switch (target)
        {
            case TargetType.Big:
                _points += 1;
                break;
            case TargetType.Medium:
                _points += 2;
                break;
            case TargetType.Small:
                _points += 3;
                break;
        }

        OnPointsChanged?.Invoke(_points);
    }

    public void SpendPoints(uint range)
    {
        _points -= range;
        OnPointsChanged?.Invoke(_points);
    }

    public float GetScoreNormalized() => (float) _points / _maxPoints;

    public event Action<uint> OnPointsChanged;
}
