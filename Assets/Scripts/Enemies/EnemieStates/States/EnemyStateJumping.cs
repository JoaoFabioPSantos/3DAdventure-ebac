using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Studio.StateMachine;

public class EnemyStateJumping : StateBase
{
    public override void OnStateEnter(EnemyControllerSM enemyController)
    {
        enemyController.StopMoving();
        enemyController.Jumping();
        Debug.Log("Jumping");
    }
}
