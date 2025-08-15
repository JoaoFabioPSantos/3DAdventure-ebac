using Studio.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CMStateJumping : StateBase
{
    public override void OnStateEnter(CharacterAnimation characterAnimation)
    {
        characterAnimation.PlayJumping();
        Debug.Log("Jumping");
    }
}
