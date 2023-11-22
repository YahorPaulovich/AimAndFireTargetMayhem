using TMPro;
using UnityEngine;
using Zenject;

public class WaveSpawnerSystem : MonoBehaviour
{
    private IWaveSpawnerFactory _waveSpawnerFactory;
    private WaveSpawnerCatalogConfig _waveSpawnerCatalogConfig;
    private TargetCollection _targetCollection;
    private PointsStorage _pointsStorage;
    private WaveExecutionProgress _waveExecutionProgress;

    [SerializeField]
    private GameObject _mainPanel;

    [SerializeField]
    private GameObject _defeatPanel;

    [SerializeField]
    private TextMeshProUGUI _scoreText;

    [Inject]
    public void Construct(
        IWaveSpawnerFactory waveSpawnerFactory, 
        WaveSpawnerCatalogConfig waveUpgradeCatalogConfig,
        TargetCollection targetCollection,
        PointsStorage pointsStorage, 
        WaveExecutionProgress waveExecutionProgress)
    {
        _waveSpawnerFactory = waveSpawnerFactory;
        _waveSpawnerCatalogConfig = waveUpgradeCatalogConfig;
        _targetCollection = targetCollection;
        _pointsStorage = pointsStorage;
        _waveExecutionProgress = waveExecutionProgress;
    }

    private void OnEnable()
    {
        _waveSpawnerFactory.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _waveSpawnerFactory.OnGameOver -= OnGameOver;
    }

    private void OnGameOver()
    {
        ShowDefeatPanel();
    }

    private void Awake()
    {
        StartSpawn();
    }

    private void StartSpawn()
    {
        StartCoroutine(
            _waveSpawnerFactory
            .SpawnWaves
            (
            _waveSpawnerCatalogConfig, 
            _targetCollection, 
            _pointsStorage, 
            _waveExecutionProgress
            ));
    }

    private void ShowDefeatPanel()
    {
        if (_defeatPanel)
        {
            _defeatPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Defeat panel not found!");
        }

        if (_mainPanel)
        {
            _mainPanel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Main panel not found!");
        }

        ShowScoreText();
    }

    private void ShowMainPanel()
    {
        if (_mainPanel)
        {
            _mainPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Main panel not found!");
        }

        if (_defeatPanel)
        {
            _defeatPanel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Defeat panel not found!");
        }
    }

    private void ShowScoreText()
    {
        if (_scoreText)
        {
            _scoreText.text = _pointsStorage.Points.ToString();
        }
    }

    public void Restart()
    {        
        _pointsStorage.SetupPoints(0);
        ShowMainPanel();
        StartSpawn();
    }
}
