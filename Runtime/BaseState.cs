#region

using System;
using UnityEngine;

#endregion

namespace State_Machine_Creator.Runtime
{
    public abstract class BaseState<TState> where TState : Enum
    {
        protected BaseState(TState key)
        {
            StateKey = key;
        }

        public TState PreviousStateKey { get; private set; }
        public TState StateKey { get; }

        public virtual void EnterState(TState previousStateKey)
        {
            PreviousStateKey = previousStateKey;
        }
        public void ExitState() { }
        public void UpdateState() { }
        public void FixedUpdateState() { }
        public TState GetNextState() { return StateKey; }
        public void OnTriggerEnter(Collider other) { }
        public void OnTriggerStay(Collider other) { }
        public void OnTriggerExit(Collider other) { }
        public void OnCollisionEnter(Collision collision) { }
        public void OnCollisionStay(Collision collision) { }
        public void OnCollisionExit(Collision collision) { }
        public void OnTriggerEnter2D(Collider2D other) { }
        public void OnTriggerStay2D(Collider2D other) { }
        public void OnTriggerExit2D(Collider2D other) { }
        public void OnCollisionEnter2D(Collision2D collision) { }
        public void OnCollisionStay2D(Collision2D collision) { }
        public void OnCollisionExit2D(Collision2D collision) { }
    }
}
