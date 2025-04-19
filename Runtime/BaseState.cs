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
        public virtual void ExitState() { }
        public virtual void UpdateState() { }
        public virtual void FixedUpdateState() { }
        public virtual TState GetNextState() { return StateKey; }
        public virtual void OnTriggerEnter(Collider other) { }
        public virtual void OnTriggerStay(Collider other) { }
        public virtual void OnTriggerExit(Collider other) { }
        public virtual void OnCollisionEnter(Collision collision) { }
        public virtual void OnCollisionStay(Collision collision) { }
        public virtual void OnCollisionExit(Collision collision) { }
        public virtual void OnTriggerEnter2D(Collider2D other) { }
        public virtual void OnTriggerStay2D(Collider2D other) { }
        public virtual void OnTriggerExit2D(Collider2D other) { }
        public virtual void OnCollisionEnter2D(Collision2D collision) { }
        public virtual void OnCollisionStay2D(Collision2D collision) { }
        public virtual void OnCollisionExit2D(Collision2D collision) { }
    }
}
