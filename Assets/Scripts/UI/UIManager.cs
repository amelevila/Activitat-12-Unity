using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _livesText;
    [SerializeField] private GameObject      _gameOverPanel;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start()
    {
        var gm = GameManager.Instance;
        if (gm != null)
        {
            gm.OnScoreChanged += RefreshScore;
            gm.OnLivesChanged += RefreshLives;
            gm.OnGameOver     += ShowGameOver;
            RefreshScore(gm.Score);
            RefreshLives(gm.Lives);
        }
        _gameOverPanel?.SetActive(false);
    }

    private void OnDestroy()
    {
        var gm = GameManager.Instance;
        if (gm == null) return;
        gm.OnScoreChanged -= RefreshScore;
        gm.OnLivesChanged -= RefreshLives;
        gm.OnGameOver     -= ShowGameOver;
    }

    private void RefreshScore(int score) => _scoreText.text = $"PUNTS: {score:D3}";
    private void RefreshLives(int lives)  => _livesText.text = $"VIDES: {new string('♥', lives)}";
    private void ShowGameOver()           => _gameOverPanel?.SetActive(true);

    public void OnRestartButton() => GameManager.Instance?.RestartGame();
}
