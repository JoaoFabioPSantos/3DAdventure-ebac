using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JetBrains.Annotations;

public class CharacterAnimation : MonoBehaviour
{
    public Animator animator;
    public Transform transform;

    public void PlayIdle()
    {
        animator.SetBool("isRunning", false);
    }

    public void PlayRun()
    {
        animator.SetBool("isRunning", true);
        transform.DOMoveX(2f, 3f);
    }

    public void PlayJumping() 
    {
        animator.SetBool("isRunning", true);
        transform.DOMoveY(2f, 1f);
    }
}
