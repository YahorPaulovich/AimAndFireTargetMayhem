using TMPro;
using UnityEngine;

public sealed class PointsWidget : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _pointText;
    private PointsStorage _pointStorage;

    private void OnEnable()
    {
        _pointStorage.OnPointsChanged += OnPointsChanged;
    }

    private void OnDisable()
    {
        _pointStorage.OnPointsChanged -= OnPointsChanged;
    }

    private void OnPointsChanged(uint points)
    {
        UpdateText(points);
    }

    private void UpdateText(uint points)
    {
        _pointText.text = points.ToString();
    }
}
