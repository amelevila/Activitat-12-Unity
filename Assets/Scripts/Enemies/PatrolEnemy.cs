using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    [SerializeField] private float _speed        = 2f;
    [SerializeField] private Transform _leftPoint;
    [SerializeField] private Transform _rightPoint;

    private SpriteRenderer _sr;
    private bool _movingRight = true;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float target = _movingRight ? _rightPoint.position.x : _leftPoint.position.x;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(target, transform.position.y), _speed * Time.deltaTime);
        _sr.flipX = _movingRight;

        if (_movingRight && transform.position.x >= _rightPoint.position.x)
            _movingRight = false;
        else if (!_movingRight && transform.position.x <= _leftPoint.position.x)
            _movingRight = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        if (collision.TryGetComponent<PlayerHealth>(out var health))
            health.TakeDamage(transform.position);
    }
}
