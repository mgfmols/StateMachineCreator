#region

using System;
using State_Machine_Creator.Runtime.Context;
using UnityEngine;

#endregion

namespace State_Machine_Creator.Runtime.Machines
{
    public class GenericStateMachine<T> : StateManager<T> where T : Enum
    {
        [field: SerializeField]
        public GenericContext<T> context { get; set; }
    }
}
