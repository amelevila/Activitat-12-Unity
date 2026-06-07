using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int   _scoreValue = 10;
    [SerializeField] private float _bobSpeed   = 2f;
    [SerializeField] private float _bobHeight  = 0.15f;
    [SerializeField] private ParticleSystem _collectFX;

    private bool  _collected;
    private float _timeOffset;
    private Vector3 _startPos;

    private void Start()
    {
        _timeOffset = Random.Range(0f, Mathf.PI * 2f);
        _startPos   = transform.position;
    }

    private void Update()
    {
        transform.position = _startPos + Vector3.up * Mathf.Sin(Time.time * _bobSpeed + _timeOffset) * _bobHeight;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_collected || !collision.CompareTag("Player")) return;
        _collected = true;

        GameManager.Instance?.AddScore(_scoreValue);
        AudioManager.Instance?.PlayCoin();

        if (_collectFX != null)
        {
            _collectFX.transform.SetParent(null);
            _collectFX.Play();
            Destroy(_collectFX.gameObject, _collectFX.main.duration + 0.5f);
        }

        Destroy(gameObject);
    }
}
