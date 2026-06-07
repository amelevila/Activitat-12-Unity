using UnityEngine;
using System.Collections;

public class PlayerPowerUp : MonoBehaviour
{
    public float SpeedMultiplier { get; private set; } = 1f;
    public bool  HasDoubleJump  { get; private set; }

    private bool      _doubleJumpUsed;
    private Coroutine _speedCoroutine;
    private Coroutine _jumpCoroutine;

    public void ActivateSpeedBoost(float duration)
    {
        if (_speedCoroutine != null) StopCoroutine(_speedCoroutine);
        _speedCoroutine = StartCoroutine(SpeedRoutine(duration));
    }

    public void ActivateDoubleJump(float duration)
    {
        if (_jumpCoroutine != null) StopCoroutine(_jumpCoroutine);
        _jumpCoroutine = StartCoroutine(DoubleJumpRoutine(duration));
    }

    public bool UseDoubleJump()
    {
        if (!HasDoubleJump || _doubleJumpUsed) return false;
        _doubleJumpUsed = true;
        return true;
    }

    public void ResetDoubleJump()
    {
        _doubleJumpUsed = false;
    }

    private IEnumerator SpeedRoutine(float duration)
    {
        SpeedMultiplier = 1.6f;
        yield return new WaitForSeconds(duration);
        SpeedMultiplier = 1f;
    }

    private IEnumerator DoubleJumpRoutine(float duration)
    {
        HasDoubleJump = true;
        yield return new WaitForSeconds(duration);
        HasDoubleJump   = false;
        _doubleJumpUsed = false;
    }
}
