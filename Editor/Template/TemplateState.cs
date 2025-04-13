#region

using State_Machine_Creator.Runtime;

#endregion

namespace State_Machine_Creator.Editor.Template
{
    public class TemplateState : BaseState<TemplateStateMachine.ETemplateState>
    {
        protected readonly TemplateContext Context;

        public TemplateState(TemplateContext context, TemplateStateMachine.ETemplateState stateKey) : base(stateKey)
        {
            Context = context;
        }
    }
}