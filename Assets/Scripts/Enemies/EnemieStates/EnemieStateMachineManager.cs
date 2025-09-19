using NaughtyAttributes;
using Studio.StateMachine;
using Studio.Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemieStateMachineManager : Singleton<EnemieStateMachineManager>
{
    public enum EnemieStates
    {
        IDLE,
        RUNNING,
        JUMPING
    }

    public StateMachine<EnemieStates> stateMachine;
    public EnemyControllerSM enemyController;
    public KeyCode keyCode = KeyCode.S;
    private int _currentState;

    public void Start()
    {
        Init();
    }

    public void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            NextState();
        }
    }


    public void Init()
    {
        stateMachine = new StateMachine<EnemieStates>();
        stateMachine.Init();
        stateMachine.RegisterStates(EnemieStates.IDLE, new EnemyStateIdle(), enemyController);
        stateMachine.RegisterStates(EnemieStates.RUNNING, new EnemyStateMoving(), enemyController);
        stateMachine.RegisterStates(EnemieStates.JUMPING, new EnemyStateJumping(), enemyController);
    }

    [Button]
    public void NextState()
    {
        if (!EditorApplication.isPlaying) return;
        _currentState += 1;
        switch (_currentState)
        {
            case 1:
                stateMachine.SwitchState(EnemieStates.RUNNING);
                break;
            case 2:
                stateMachine.SwitchState(EnemieStates.IDLE);
                break;
            case 3:
                stateMachine.SwitchState(EnemieStates.JUMPING);
                break;
            case 4:
                stateMachine.SwitchState(EnemieStates.IDLE);
                _currentState = 0;
                break;
        }
    }
}
