using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class CharacterAnimation : MonoBehaviour
{
    public Animator animatorCharacter;
    public Transform transformCharacter;

    public void PlayIdle()
    {
        animatorCharacter.SetBool("isRunning", false);
    }

    public void PlayRun()
    {
        animatorCharacter.SetBool("isRunning", true);
        transformCharacter.DOMoveX(2f, 2f);
    }

    public void PlayJumping() 
    {
        animatorCharacter.SetBool("isRunning", true);
        transformCharacter.DOMoveY(2f, 1f);
    }
}
