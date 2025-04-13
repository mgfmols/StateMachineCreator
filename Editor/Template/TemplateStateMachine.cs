#region

using State_Machine_Creator.Runtime;
using UnityEngine;

#endregion

namespace State_Machine_Creator.Editor.Template
{
    public class TemplateStateMachine : StateManager<TemplateStateMachine.ETemplateState>
    {
        public enum ETemplateState
        {
            Template,
        }

        [SerializeField] private TemplateContext context;
        
        private void Awake()
        {
            InitializeStates();
        } 

        private void InitializeStates()
        {
            States.Add(ETemplateState.Template, new TemplateState(context, ETemplateState.Template));
            SetStartState(ETemplateState.Template);
        }
    }
}
