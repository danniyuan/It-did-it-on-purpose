using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorActions : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetBoolTrue(string animParam)
    {
        animator.SetBool(animParam, true);
    }

    public void SetBoolFalse(string animParam)
    {
        animator.SetBool(animParam, false);
    }
}
