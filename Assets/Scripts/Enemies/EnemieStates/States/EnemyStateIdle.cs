using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Studio.StateMachine;

public class EnemyStateIdle : StateBase
{
    public override void OnStateEnter(EnemyControllerSM enemyController)
    {
        enemyController.StopMoving();
        enemyController.StopJump();
        enemyController.Idle();
        Debug.Log("Idle");
    }
}
