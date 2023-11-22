using UnityEngine;
using UnityEngine.UI;

public sealed class WaveSpawnProgressPanel : MonoBehaviour
{
    private const float SPENT_SCORE_FADE_TIMER_MAX = 0.6f;

    [SerializeField] private Image _barImage;
    [SerializeField] private Image _spentBarImage;

    private Color _spentColor;
    private float _spentProgressFadeTimer;

    private void Awake()
    {
        _spentColor = _spentBarImage.color;
        _spentColor.a = 0f;
        _spentBarImage.color = _spentColor;
    }

    private void Update()
    {
        if (_spentColor.a > 0f)
        {
            _spentProgressFadeTimer -= Time.deltaTime;
            if (_spentProgressFadeTimer < 0f)
            {
                float fadeAmount = 5f;
                _spentColor.a -= fadeAmount * Time.deltaTime;
                _spentBarImage.color = _spentColor;
            }
        }
    }

    public void UpdateProgressUI(float progress)
    {
        if (_spentColor.a <= 0f)
        {
            _spentBarImage.fillAmount = _barImage.fillAmount;
        }
        _spentColor.a = 1f;
        _spentBarImage.color = _spentColor;
        _spentProgressFadeTimer = SPENT_SCORE_FADE_TIMER_MAX;

        _barImage.fillAmount = progress;
    }
}
