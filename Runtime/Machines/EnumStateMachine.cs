#region

using System;
using System.Linq;

#endregion

namespace State_Machine_Creator.Runtime.Machines
{
    public sealed class EnumStateMachine<T> : GenericStateMachine<T> where T : Enum
    {
        private void Awake()
        {
            InitializeStates();
        }

        private void InitializeStates()
        {
            var values = Enum.GetValues(typeof(T)).Cast<T>().ToList();
            foreach (var enumValue in values)
            {
                States.Add(enumValue, new GenericState<T>(context, enumValue));
            }
            SetStartState(values[0]);
        }
    }
}