#region

using System;
using State_Machine_Creator.Runtime.Context;
using UnityEngine.InputSystem;

#endregion

namespace State_Machine_Creator.Runtime.Player
{
    public abstract class InputBaseState<TState> : BaseState<TState> where TState : Enum
    {
        protected readonly InputContext Context;

        protected InputBaseState(TState key, InputContext context) : base(key)
        {
            Context = context;
        }

        protected void InputHookPerformed(string inputMap, Action<InputAction.CallbackContext> code)
        {
            Context.Input.currentActionMap[inputMap].performed += code;
        }

        protected void InputUnhookPerformed(string inputMap, Action<InputAction.CallbackContext> code)
        {
            Context.Input.currentActionMap[inputMap].performed -= code;
        }

        protected void InputHookCancelled(string inputMap, Action<InputAction.CallbackContext> code)
        {
            Context.Input.currentActionMap[inputMap].canceled += code;
        }

        protected void InputUnhookCancelled(string inputMap, Action<InputAction.CallbackContext> code)
        {
            Context.Input.currentActionMap[inputMap].canceled -= code;
        }

        protected void InputHookStarted(string inputMap, Action<InputAction.CallbackContext> code)
        {
            Context.Input.currentActionMap[inputMap].started += code;
        }

        protected void InputUnhookStarted(string inputMap, Action<InputAction.CallbackContext> code)
        {
            Context.Input.currentActionMap[inputMap].started -= code;
        }
    }
}