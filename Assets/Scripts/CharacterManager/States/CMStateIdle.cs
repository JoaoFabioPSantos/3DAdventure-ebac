using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Studio.StateMachine;
using UnityEngine.TextCore.Text;

public class CMStateIdle : StateBase
{
    public override void OnStateEnter()
    {
        Debug.Log("Idle");
    }

}
