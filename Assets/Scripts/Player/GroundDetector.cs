using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private Vector2 _checkSize = new Vector2(0.4f, 0.1f);
    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private bool _isGrounded;
    public bool IsGrounded => _isGrounded;

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapBox(transform.position, _checkSize, 0f, _groundMask);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, _checkSize);
    }
}
