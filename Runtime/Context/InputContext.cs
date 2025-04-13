#region

using System;
using EditorAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

#endregion

namespace State_Machine_Creator.Runtime.Context
{
    [Serializable]
    public class InputContext
    {
        [field: SerializeField, Required(true, false)]
        public PlayerInput Input { get; set; }
    }
}