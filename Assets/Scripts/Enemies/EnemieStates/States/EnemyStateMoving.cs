using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Studio.StateMachine;

public class EnemyStateMoving : StateBase
{
    public override void OnStateEnter(EnemyControllerSM enemyController)
    {
        enemyController.StopJump();
        enemyController.Moving();
        Debug.Log("Moving");
    }
}
