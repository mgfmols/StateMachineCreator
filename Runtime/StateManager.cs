#region

using System;
using System.Collections.Generic;
using EditorAttributes;
using UnityEngine;

#endregion

namespace State_Machine_Creator.Runtime
{
    public abstract class StateManager<TState> : MonoBehaviour where TState : Enum
    {
        [SerializeField, DisableInEditMode, DisableInPlayMode] protected TState state;
        protected readonly Dictionary<TState, BaseState<TState>> States = new();
        private BaseState<TState> _currentState;
        private BaseState<TState> _previousState;

        private bool _isTransitioningState;

        private void Start()
        {
            _currentState.EnterState(_previousState.StateKey);
        }

        private void OnDestroy()
        {
            _currentState.ExitState();
        }

        private void Update()
        {
            if (StateUnchanged(out var nextStateKey))
            {
                _currentState.UpdateState();
            }
            else
            {
                TransitionToState(nextStateKey);
            }
        }

        private void FixedUpdate()
        {
            if (StateUnchanged(out var nextStateKey))
            {
                _currentState.FixedUpdateState();
            }
            else
            {
                TransitionToState(nextStateKey);
            }
        }

        protected void SetStartState(TState stateKey)
        {
            _previousState = States[stateKey];
            _currentState = States[stateKey];
            state = stateKey;
        }

        private bool StateUnchanged(out TState nextStateKey)
        {
            nextStateKey = _currentState.GetNextState();
            return !_isTransitioningState && nextStateKey.Equals(_currentState.StateKey);
        }

        private void TransitionToState(TState stateKey)
        {
            _isTransitioningState = true;
            _currentState.ExitState();
            _previousState = _currentState;
            _currentState = States[stateKey];
            _currentState.EnterState(_previousState.StateKey);
            _isTransitioningState = false;
            state = stateKey;
        }

        public void SwitchState(TState newState)
        {
            if (States.ContainsKey(newState))
            {
                TransitionToState(newState);    
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            _currentState.OnTriggerEnter(other);
        }

        private void OnTriggerStay(Collider other)
        {
            _currentState.OnTriggerStay(other);
        }

        private void OnTriggerExit(Collider other)
        {
            _currentState.OnTriggerExit(other);
        }

        private void OnCollisionEnter(Collision collision)
        {
            _currentState.OnCollisionEnter(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            _currentState.OnCollisionStay(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            _currentState.OnCollisionExit(collision);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _currentState.OnTriggerEnter2D(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            _currentState.OnTriggerStay2D(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _currentState.OnTriggerExit2D(other);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _currentState.OnCollisionEnter2D(collision);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            _currentState.OnCollisionStay2D(collision);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            _currentState.OnCollisionExit2D(collision);
        }
    }
}
