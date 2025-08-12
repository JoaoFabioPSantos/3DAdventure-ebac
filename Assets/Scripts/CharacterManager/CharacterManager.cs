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
        stateMachine.RegisterStates(CharacterStates.IDLE, new CMStateIdle());
        stateMachine.RegisterStates(CharacterStates.RUNNING, new CMStateRunning());
        stateMachine.RegisterStates(CharacterStates.JUMPING, new CMStateJumping());
    }

    [Button]
    public void NextState()
    {
        if (!EditorApplication.isPlaying) return;
        _currentState += 1;
        switch (_currentState)
        {
            case 1:
                stateMachine.SwitchState(CharacterStates.RUNNING);
                characterAnimation.PlayRun();
                break;
            case 2:
                stateMachine.SwitchState(CharacterStates.IDLE);
                characterAnimation.PlayIdle();
                break;
            case 3:
                stateMachine.SwitchState(CharacterStates.JUMPING);
                _currentState = 0;
                characterAnimation.PlayJumping();
                break;
        }
    }
}
