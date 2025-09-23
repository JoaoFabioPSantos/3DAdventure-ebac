using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Studio.Core.Singleton;
using Studio.StateMachine;
using NaughtyAttributes;
using UnityEditor;

public class CharacterManager : Singleton<CharacterManager>
{
    private int _currentState;
    public CharacterAnimation characterAnimation;

    public enum CharacterStates
    {
        IDLE,
        RUNNING,
        JUMPING
    }

    public StateMachine<CharacterStates> stateMachine;

    public void Start()
    {
        Init();
    }

    public void Init()
    {
        stateMachine = new StateMachine<CharacterStates>();
        stateMachine.Init();
        stateMachine.RegisterStates(CharacterStates.IDLE, new CMStateIdle(), characterAnimation);
        stateMachine.RegisterStates(CharacterStates.RUNNING, new CMStateRunning(), characterAnimation);
        stateMachine.RegisterStates(CharacterStates.JUMPING, new CMStateJumping(), characterAnimation);
    }

    public void NextState()
    { 
        _currentState += 1;
        switch (_currentState)
        {
            case 1:
                stateMachine.SwitchState(CharacterStates.RUNNING);
                break;
            case 2:
                stateMachine.SwitchState(CharacterStates.IDLE);
                break;
            case 3:
                stateMachine.SwitchState(CharacterStates.JUMPING);
                _currentState = 0;
                break;
        }
    }
}
