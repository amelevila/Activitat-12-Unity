using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _maxSpeed     = 7f;
    [SerializeField] private float _acceleration = 10f;
    [SerializeField] private float _deceleration = 15f;
    [SerializeField] private float _jumpForce    = 15f;
    [SerializeField] private GroundDetector _ground;

    private Rigidbody2D    _rb;
    private Animator       _anim;
    private SpriteRenderer _sr;
    private PlayerPowerUp  _powerUp;

    private static readonly int _hashSpeed      = Animator.StringToHash("Speed");
    private static readonly int _hashIsGrounded = Animator.StringToHash("IsGrounded");
    private static readonly int _hashVelocityY  = Animator.StringToHash("VelocityY");
    private static readonly int _hashJump       = Animator.StringToHash("Jump");

    private void Awake()
    {
        _rb      = GetComponent<Rigidbody2D>();
        _anim    = GetComponent<Animator>();
        _sr      = GetComponent<SpriteRenderer>();
        TryGetComponent<PlayerPowerUp>(out _powerUp);
    }

    private void Update()
    {
        bool grounded = _ground.IsGrounded;
        if (grounded) _powerUp?.ResetDoubleJump();

        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpForce);
                _anim.SetTrigger(_hashJump);
            }
            else if (_powerUp != null && _powerUp.UseDoubleJump())
            {
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpForce);
                _anim.SetTrigger(_hashJump);
            }
        }

        if (Input.GetButtonUp("Jump") && _rb.linearVelocity.y > 0f)
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _rb.linearVelocity.y * 0.45f);

        _anim.SetFloat(_hashSpeed, Mathf.Abs(_rb.linearVelocity.x));
        _anim.SetBool(_hashIsGrounded, grounded);
        _anim.SetFloat(_hashVelocityY, _rb.linearVelocity.y);
    }

    private void FixedUpdate()
    {
        float input  = Input.GetAxis("Horizontal");
        float speed  = _maxSpeed * (_powerUp != null ? _powerUp.SpeedMultiplier : 1f);
        float target = input * speed;
        float rate   = input != 0f ? _acceleration : _deceleration;
        _rb.linearVelocity = new Vector2(
            Mathf.Lerp(_rb.linearVelocity.x, target, rate * Time.fixedDeltaTime),
            _rb.linearVelocity.y
        );

        if (input != 0f) _sr.flipX = input < 0f;
    }

    public void BounceUp(float force = 12f)
    {
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, force);
    }
}
