using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private int _startingLives = 3;

    public int Score { get; private set; }
    public int Lives { get; private set; }

    public event Action<int> OnScoreChanged;
    public event Action<int> OnLivesChanged;
    public event Action OnGameOver;

    private bool _gameOver;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Lives = _startingLives;
    }

    public void AddScore(int amount)
    {
        if (_gameOver) return;
        Score += amount;
        OnScoreChanged?.Invoke(Score);
    }

    public void PlayerDied()
    {
        Lives--;
        OnLivesChanged?.Invoke(Lives);
        if (Lives <= 0)
        {
            _gameOver = true;
            OnGameOver?.Invoke();
        }
        else
        {
            Invoke(nameof(RespawnPlayer), 2f);
        }
    }

    private void RespawnPlayer()
    {
        var player = FindFirstObjectByType<PlayerController>();
        if (player == null) return;
        player.transform.position = Vector3.zero;
    }

    public void RestartGame()
    {
        _gameOver = false;
        Score = 0;
        Lives = _startingLives;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
