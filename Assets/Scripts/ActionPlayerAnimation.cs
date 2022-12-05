using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPlayerAnimation : MonoBehaviour
{
    private const string ANIMATOR_TRIGGER_DEATH = "Death";

    [SerializeField] private Animator _animator;

    public void Dead()
    {
        _animator.SetTrigger(ANIMATOR_TRIGGER_DEATH);
    }

}
