#region

using System;
using State_Machine_Creator.Runtime.Context;
using UnityEngine.InputSystem;

#endregion

namespace State_Machine_Creator.Runtime
{
    public class GenericState<T> : BaseState<T> where T : Enum
    {
        protected readonly GenericContext<T> Context;

        public GenericState(GenericContext<T> context, T stateKey) : base(stateKey)
        {
            Context = context;
        }
        
        protected void PlayerInputHook(string inputMap, Action<InputAction.CallbackContext> code)
        {
            Context.Input.currentActionMap[inputMap].performed += code;
        }

        protected void PlayerInputUnhook(string inputMap, Action<InputAction.CallbackContext> code)
        {
            Context.Input.currentActionMap[inputMap].performed -= code;
        }
    }
}