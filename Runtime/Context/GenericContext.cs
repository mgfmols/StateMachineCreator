using UnityEngine;
using UnityEngine.InputSystem;

namespace State_Machine_Creator.Runtime.Context
{
    public abstract class GenericContext<T>
    {
        [field: SerializeField]
        public PlayerInput Input { get; set; }
    }
}