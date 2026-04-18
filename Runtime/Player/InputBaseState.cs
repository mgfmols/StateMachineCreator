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
            if (TryGetInputAction(inputMap, out var action))
            {
                action.performed += code;
            }
        }

        protected void InputUnhookPerformed(string inputMap, Action<InputAction.CallbackContext> code)
        {
            if (TryGetInputAction(inputMap, out var action))
            {
                action.performed -= code;
            }
        }

        protected void InputHookCancelled(string inputMap, Action<InputAction.CallbackContext> code)
        {
            if (TryGetInputAction(inputMap, out var action))
            {
                action.canceled += code;
            }
        }

        protected void InputUnhookCancelled(string inputMap, Action<InputAction.CallbackContext> code)
        {
            if (TryGetInputAction(inputMap, out var action))
            {
                action.canceled -= code;
            }
        }

        protected void InputHookStarted(string inputMap, Action<InputAction.CallbackContext> code)
        {
            if (TryGetInputAction(inputMap, out var action))
            {
                action.started += code;
            }
        }

        protected void InputUnhookStarted(string inputMap, Action<InputAction.CallbackContext> code)
        {
            if (TryGetInputAction(inputMap, out var action))
            {
                action.started -= code;
            }
        }

        private bool TryGetInputAction(string inputMap, out InputAction inputAction)
        {
            inputAction = null;
            
            var input = Context.Input;
            if (!input)
            {
                return false;
            }

            var actionMap = input.currentActionMap;
            if (actionMap == null)
            {
                return false;
            }

            var action = actionMap.FindAction(inputMap);
            if (action == null)
            {
                return false;
            }
            inputAction = action;
            return true;
        }
    }
}