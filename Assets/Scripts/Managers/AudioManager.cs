using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [SerializeField] private AudioClip _musicClip;
    [SerializeField] private AudioClip _coinClip;
    [SerializeField] private AudioClip _powerUpClip;
    [SerializeField] private AudioClip _jumpClip;
    [SerializeField] private AudioClip _landClip;
    [SerializeField] private AudioClip _hurtClip;
    [SerializeField] private AudioClip _deathClip;
    [SerializeField] private AudioClip _enemyDeathClip;
    [SerializeField] private AudioClip _enemyAlertClip;
    [SerializeField] private AudioClip _trapClip;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (_musicClip != null)
        {
            _musicSource.clip = _musicClip;
            _musicSource.loop = true;
            _musicSource.Play();
        }
    }

    private void PlaySFX(AudioClip clip)
    {
        if (clip != null) _sfxSource.PlayOneShot(clip);
    }

    public void PlayCoin()        => PlaySFX(_coinClip);
    public void PlayPowerUp()     => PlaySFX(_powerUpClip);
    public void PlayJump()        => PlaySFX(_jumpClip);
    public void PlayLand()        => PlaySFX(_landClip);
    public void PlayHurt()        => PlaySFX(_hurtClip);
    public void PlayDeath()       => PlaySFX(_deathClip);
    public void PlayEnemyDeath()  => PlaySFX(_enemyDeathClip);
    public void PlayEnemyAlert()  => PlaySFX(_enemyAlertClip);
    public void PlayTrap()        => PlaySFX(_trapClip);
}
