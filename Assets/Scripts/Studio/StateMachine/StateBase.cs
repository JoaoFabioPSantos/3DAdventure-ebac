using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Studio.StateMachine
{
    public class StateBase
    {
        public virtual void OnStateEnter(CharacterAnimation characterAnimation)
        {
            Debug.Log("OnStateEnter");
        }

        public virtual void OnStateEnter(EnemyControllerSM enemyController)
        {
            Debug.Log("OnStateEnter");
        }

        public virtual void OnStateEnter(params object[] objs)
        {
            Debug.Log("OnStateEnter");
        }

        public virtual void OnStateStay()
        {
            Debug.Log("OnStateStay");
        }
        public virtual void OnStateExit()
        {
            //Debug.Log("OnStateExit");
        }

        public virtual void OnStateExit(EnemyControllerSM enemyController)
        {
            Debug.Log("OnStateExit");
        }

    }

}