using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Running;

namespace StateMachine.Node
{
    public class AdvanceState : NodeState
    {
        private Step Step;
        private bool HasRequestedAdvance=false;
        public AdvanceState(NodeStateMachine stateMachine,Step step) : base(stateMachine)
        {
            Step = step;
            if (step == null)
            {
                Debug.LogError("AdvanceState : Step is null !");
            }
        }
        public override void Enter()
        {
            base.Enter();
            EventBus.Subscript<ERequestNextStep>(OnRequestAdvance);
            EventBus.Subscript<ERequestStepByID>(OnRequestStepByID);
            InputManager.Instance.InputStateMachine.SwitchState(new StepInputState());
            Step.ComponentsList.ForEach(component =>
            {
                if(component == null)
                {
                    Debug.LogError("Some Component is null");
                    return;
                }
                component.Enter(StateMachine.Node);
            });
        }
        public override void Exit()
        {
            base.Exit();
            Step.ComponentsList.ForEach(component =>
            {
                if (component == null)
                {
                    Debug.LogError("Some Component is null");
                    return;
                }
                component.Exit(StateMachine.Node);
            });
            EventBus.Unsubscript<ERequestNextStep>(OnRequestAdvance);
            EventBus.Unsubscript<ERequestStepByID>(OnRequestStepByID);
        }
        private void OnRequestAdvance(ERequestNextStep e)
        {
            if (HasRequestedAdvance)
            {
                return;
            }
            HasRequestedAdvance = true;
            StateMachine.Advance();
        }
        private void OnRequestStepByID(ERequestStepByID e)
        {
            if (HasRequestedAdvance)
            {
                return;
            }
            //if (e.NextID == null)
            //{
            //    return;
            //}
            HasRequestedAdvance = true;
            StateMachine.Advance(e.NextID);
        }
    }
}