using Studio.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CMStateRunning : StateBase
{
    public override void OnStateEnter()
    {
        Debug.Log("Run");
    }

}
