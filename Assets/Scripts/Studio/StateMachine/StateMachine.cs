using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEditor;

namespace Studio.StateMachine
{
    public class StateMachine<T> where T : System.Enum
    {

        //a chave do dicionário e o nome
        public Dictionary<T, StateBase> dictionaryState;

        private StateBase _currentState;
        private CharacterAnimation _currentCharacterAnimation;
        public float timeToStartGame = 1f;

        public StateBase currentState
        {
            get { return _currentState; }
        }

        public void Init()
        {
            dictionaryState = new Dictionary<T, StateBase>();
        }

        public void RegisterStates(T typeEnum, StateBase state, CharacterAnimation characterAnimation)
        {
            if (characterAnimation != null)_currentCharacterAnimation = characterAnimation;
            dictionaryState.Add(typeEnum, state);
        }

        public void RegisterStates(T typeEnum, StateBase state)
        {
            dictionaryState.Add(typeEnum, state);
        }

        public void SwitchState(T state)
        { 
            if (_currentState != null) _currentState.OnStateExit();

            _currentState = dictionaryState[state];

            if (_currentCharacterAnimation != null)
            {
                _currentState.OnStateEnter(_currentCharacterAnimation);
            }
            else{
                _currentState.OnStateEnter();
            }
        }

        public void Update()
        {
            if (_currentState != null) _currentState.OnStateStay();
        }
    }

}