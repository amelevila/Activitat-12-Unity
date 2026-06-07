using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int   _maxHealth        = 3;
    [SerializeField] private float _invincibleDuration = 1.5f;
    [SerializeField] private float _knockbackX       = 5f;
    [SerializeField] private float _knockbackY       = 6f;

    private PlayerController _controller;
    private Rigidbody2D      _rb;
    private SpriteRenderer   _sr;

    private bool _dead;
    private bool _invincible;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _rb         = GetComponent<Rigidbody2D>();
        _sr         = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(Vector2 hitSourcePos)
    {
        if (_dead || _invincible) return;
        Vector2 dir = ((Vector2)transform.position - hitSourcePos).normalized;
        _rb.linearVelocity = new Vector2(dir.x * _knockbackX, _knockbackY);
        AudioManager.Instance?.PlayHurt();
        GameManager.Instance?.LoseLife();
        if (GameManager.Instance != null && GameManager.Instance.Lives <= 0)
            Die();
        else
            StartCoroutine(InvincibilityRoutine());
    }

    public void ForceKill()
    {
        if (_dead) return;
        Die();
    }

    private void Die()
    {
        _dead = true;
        _controller.enabled = false;
        _rb.linearVelocity = new Vector2(0f, _rb.linearVelocity.y);
        AudioManager.Instance?.PlayDeath();
        GameManager.Instance?.PlayerDied();
    }

    private IEnumerator InvincibilityRoutine()
    {
        _invincible = true;
        float elapsed = 0f;
        while (elapsed < _invincibleDuration)
        {
            _sr.enabled = !_sr.enabled;
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.1f;
        }
        _sr.enabled = true;
        _invincible = false;
    }

    public void Respawn()
    {
        _dead        = false;
        _invincible  = false;
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _sr.enabled  = true;
        _controller.enabled = true;
    }
}
