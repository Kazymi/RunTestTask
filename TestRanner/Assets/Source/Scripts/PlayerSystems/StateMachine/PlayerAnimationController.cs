using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController
{
    private readonly Animator _animator;
    private readonly Dictionary<PlayerAnimationType, int> _hashes;

    public PlayerAnimationController(Animator animator)
    {
        _animator = animator;
        _hashes = new Dictionary<PlayerAnimationType, int>();
        foreach (PlayerAnimationType playerAnimationType in Enum.GetValues(typeof(PlayerAnimationType)))
        {
            _hashes.Add(playerAnimationType, Animator.StringToHash(playerAnimationType.ToString()));
        }
    }

    public void SetFloat(PlayerAnimationType playerAnimationType, float value)
    {
        _animator.SetFloat(_hashes[playerAnimationType], value);
    }

    public void SetBool(PlayerAnimationType playerAnimationType, bool value)
    {
        _animator.SetBool(_hashes[playerAnimationType], value);
    }
}