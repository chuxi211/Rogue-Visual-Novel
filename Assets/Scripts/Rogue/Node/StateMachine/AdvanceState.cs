using Running;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Node
{
    public class AdvanceState : NodeState
    {
        private Step Step;
        private bool HasRequestedAdvance = false;
        private readonly List<IComponent> ActivatedComponents = new List<IComponent>();
        public AdvanceState(NodeStateMachine stateMachine, Step step) : base(stateMachine)
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
            if (Step?.ComponentsList == null)
            {
                StateMachine.SwitchState(new FailureState(StateMachine, NodeErrorCode.InvalidStep));
                return;
            }
            EventBus.Subscript<ERequestStepByID>(OnRequestStepByID);
            EventBus.Subscript<ERequestNextStep>(OnRequestNextStep);

            foreach (IComponent component in Step.ComponentsList)
            {
                if (component == null)
                {
                    StateMachine.SwitchState(new FailureState(StateMachine, NodeErrorCode.InvalidComponent));
                    return;
                }
                ActivatedComponents.Add(component);
                StateMachine.Node.AddEnteredComponent(component);
                try
                {
                    component.Enter();
                }
                catch (Exception exception)
                {
                    Debug.LogException(exception);
                    StateMachine.SwitchState(new FailureState(StateMachine, NodeErrorCode.InvalidComponent));
                    return;
                }
            }
        }
        public override void Exit()
        {
            base.Exit();
            EventBus.Unsubscript<ERequestStepByID>(OnRequestStepByID);
            EventBus.Unsubscript<ERequestNextStep>(OnRequestNextStep);
            foreach (IComponent component in ActivatedComponents)
            {
                try
                {
                    component.ExitOnStepEnd();
                }
                catch (Exception exception)
                {
                    Debug.LogException(exception);
                }
            }
        }
        private void OnRequestStepByID(ERequestStepByID e)
        {
            if (HasRequestedAdvance)
            {
                return;
            }
            if (IsCompleted())
            {
                StateMachine.Complete();
                return;
            }
            HasRequestedAdvance = true;
            StateMachine.Advance(e.NextID);
        }
        private void OnRequestNextStep(ERequestNextStep e)
        {
            if (HasRequestedAdvance)
            {
                return;
            }

            if (IsCompleted())
            {
                StateMachine.Complete();
                return;
            }
            if (!Step.NextID.IsValid)
            {
                StateMachine.SwitchState(new FailureState(StateMachine,NodeErrorCode.InvalidNextStepID));
                return;
            }
            HasRequestedAdvance = true;
            StateMachine.Advance(Step.NextID);
        }
        private bool IsCompleted()
        {
            return Step != null && Step.AdvanceType == Config.StepAdvanceType.End;
        }
    }
}
