using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { Speed, DoubleJump }

    [SerializeField] private PowerUpType _type     = PowerUpType.Speed;
    [SerializeField] private float       _duration = 5f;

    private bool _collected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_collected || !collision.CompareTag("Player")) return;
        if (!collision.TryGetComponent<PlayerPowerUp>(out var powerUp)) return;
        _collected = true;
        GameManager.Instance?.AddScore(100);

        if (_type == PowerUpType.Speed)
            powerUp.ActivateSpeedBoost(_duration);
        else
            powerUp.ActivateDoubleJump(_duration);

        AudioManager.Instance?.PlayPowerUp();
        Destroy(gameObject);
    }
}
