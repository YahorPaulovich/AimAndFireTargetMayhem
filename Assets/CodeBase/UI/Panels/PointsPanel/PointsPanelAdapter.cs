using UnityEngine;

public sealed class PointsPanelAdapter : IEnableComponent, IDisableComponent
{
    [SerializeField]
    private PointsScorePanel _view;
    private PointsStorage _storage;

    public PointsPanelAdapter(PointsScorePanel pointsView, PointsStorage pointsStorage)
    {
        _view = pointsView;
        _storage = pointsStorage;
    }

    private void OnPointsChanged(uint points)
    {
        _view.SetupPoints(points.ToString());
    }

    void IEnableComponent.OnEnable()
    {
        _storage.OnPointsChanged += OnPointsChanged;
        _view.SetupPoints(_storage.Points.ToString());
    }

    void IDisableComponent.OnDisable()
    {
        _storage.OnPointsChanged -= OnPointsChanged;
    }
}
