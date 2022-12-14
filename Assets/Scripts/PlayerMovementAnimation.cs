using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAnimation : MonoBehaviour
{
    private const string ANIMATOR_TRIGGER_JUMP_UP = "JumpUp";
    private const string ANIMATOR_TRIGGER_JUMP_DOWN = "JumpDown";

    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Animator _animator;

    private void OnEnable()
    {
        _playerMovement.OnJumpedStart += JumpAnimationStart;
        _playerMovement.OnJumpedEnd += JumpAnimationEnd;
    }

    private void JumpAnimationStart()
    {
        _animator.SetTrigger(ANIMATOR_TRIGGER_JUMP_UP);
    }

    private void JumpAnimationEnd()
    {
        _animator.SetTrigger(ANIMATOR_TRIGGER_JUMP_DOWN);
    }

    private void OnDisable()
    {
        _playerMovement.OnJumpedStart -= JumpAnimationStart;
        _playerMovement.OnJumpedEnd -= JumpAnimationEnd;

    }
}
