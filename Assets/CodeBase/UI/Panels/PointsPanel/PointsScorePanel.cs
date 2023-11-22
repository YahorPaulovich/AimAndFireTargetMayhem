using UnityEngine;
using TMPro;

public sealed class PointsScorePanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _pointText;

    public void SetupPoints(string points)
    {
        _pointText.text = points;
    }
}
