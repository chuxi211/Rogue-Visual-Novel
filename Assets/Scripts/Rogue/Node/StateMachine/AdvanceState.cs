using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Running;
using Input;

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
            EventBus.Subscript<ERequestStepByID>(OnRequestStepByID);
            EventBus.Subscript<ERequestNextStep>(OnRequestNextStep);
            
            Step.ComponentsList.ForEach(component =>
            {
                if(component == null)
                {
                    Debug.LogError("Some Component is null");
                    return;
                }
                component.Enter();
                StateMachine.Node.AddEnteredComponent(component);
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
                component.ExitOnStepEnd();
            });
            EventBus.Unsubscript<ERequestStepByID>(OnRequestStepByID);
            EventBus.Unsubscript<ERequestNextStep>(OnRequestNextStep);
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
        private void OnRequestNextStep(ERequestNextStep e)
        {
            if (HasRequestedAdvance)
            {
                Debug.Log("");
                return;
            }
            if (!Step.NextID.IsValid)
            {
                Debug.Log("");
                return;
            }
            HasRequestedAdvance = true;
            StateMachine.Advance(Step.NextID);
        }
    }
}